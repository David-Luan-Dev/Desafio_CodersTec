import { animate, state, style, transition, trigger } from '@angular/animations';
import { finalize } from 'rxjs/operators'
import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ILojaTransacoes, TipoTransacoesEnum, transacoesFinanceira } from 'src/app/Interface/ILojaTransacoes';
import { MovimentacoesFinanceiraService } from 'src/app/services/movimentacoes.service';
import { TIPO_TRANSACAO_OPTIONS } from 'src/app/shared/utils/const/tipo-transacao'
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-tela-movimentacao-financeira',
  templateUrl: './tela-movimentacao-financeira.component.html',
  styleUrls: ['./tela-movimentacao-financeira.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],

})
export class TelaMovimentacaoFinanceiraComponent implements OnInit {
  filtroNome: string = '';
  filtroTipoModal!: string;
  selectedFileName!: string;
  dataSource: MatTableDataSource<ILojaTransacoes> = new MatTableDataSource<ILojaTransacoes>([]);
  displayedColumns: string[] = ['lojaId', 'nomeDaLoja', 'donoDaLoja', 'total', 'acao'];
  IojaTransacoes: ILojaTransacoes[] = [];
  subTableDataSource: MatTableDataSource<transacoesFinanceira> = new MatTableDataSource<transacoesFinanceira>();
  subTableDisplayedColumns: string[] = [
    'idTransacao', 'lojaId', 'donoDaLoja', 'nomeDaLoja', 'tipo', 'data_Ocorrencia', 'valor',
    'cpf', 'cartao', 'hora_Ocorrencia'
  ];

  readonly TIPO_TRANSACAO_OPTIONS = TIPO_TRANSACAO_OPTIONS

  @ViewChild('modalTemplate', { static: true }) modalTemplate!: TemplateRef<any>;
  @ViewChild('fileInput', { static: false }) fileInput!: ElementRef<HTMLInputElement>;

  selectedTransacao: ILojaTransacoes | null = null;
  IsLoading: boolean = false;

  constructor(
    private movimentacoesFinanceiraService: MovimentacoesFinanceiraService,
    private dialog: MatDialog,
    readonly snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.movimentacoesFinanceiraService.getAllMovimentacoes().subscribe((lojas) => {
      this.dataSource = new MatTableDataSource(lojas.data);
    });
  }

  pesquisarPeloNomeDaLoja() {
    if (this.filtroNome != null) {
      this.filtroNome = this.filtroNome.trim(); // Remove whitespace
      this.filtroNome = this.filtroNome.toLowerCase(); // MatTableDataSource defaults to lowercase matches
      this.dataSource.filter = this.filtroNome;
    }
  }

  pesquisarPeloNomeDaLojaModal() {
    this.subTableDataSource.filterPredicate = (data: transacoesFinanceira, filter: string) => data.tipo.toString().indexOf(filter) != -1;
    this.subTableDataSource.filter = this.filtroTipoModal.toString();
  }

  getTipoTransacao(tipo: TipoTransacoesEnum): string {
    return TipoTransacoesEnum[tipo];
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFileName = input.files[0].name;
      this.IsLoading = true;
      // Adicione aqui o código para processar o arquivo, se necessário
      this.uploadFile(input.files[0]);
    } else {
      this.selectedFileName = '';
    }
  }

  private uploadFile(file: File): void {
    this.movimentacoesFinanceiraService.uploadFileCNAB(file).pipe(
      finalize(() => { this.IsLoading = false })
    )
      .subscribe({
        next: (response) => {
          if (response.data as number > 0) { // linha afetadas.
            this.getAll();
            this.open("Transações salva com sucesso", 'Fechar')
            this.clearFileSelection();
          }
          else {
            this.open(response.Mensage, 'Fechar')
          }
        },
        error: (err: any) => {
          if (err.status === 400) {
            this.open(err.error.mensagem, 'Fechar');
            this.clearFileSelection();
          } else {
            this.open("Ocorreu um erro, tente novamente", 'Fechar')
          }
        }
      });
  }

  open(message: string, action = '', config?: MatSnackBarConfig) {
    return this.snackBar.open(message, action, config);
  }

  clearFileSelection(): void {
    this.selectedFileName = '';
    // Também limpe o input file para que a alteração do mesmo arquivo possa ser detectada novamente
    if (this.fileInput) {
      this.fileInput.nativeElement.value = ''; // Limpa o valor do input de arquivo
    }
  }

  getTotalTransacoes(transacoes: transacoesFinanceira[]): number {
    return transacoes?.reduce((total, transacao) => this.Total(total, transacao), 0);
  }

  // regra aplicada no documento do desafio.
  Total(total: number, transacao: transacoesFinanceira) {
    let valorTotalDescontado = 0;
    switch (transacao.tipo) {
      case TipoTransacoesEnum.Debito:
        valorTotalDescontado = total + transacao.valor
        break;
      case TipoTransacoesEnum.Boleto:
        valorTotalDescontado = total - transacao.valor
        break;
      case TipoTransacoesEnum.Financiamento:
        valorTotalDescontado = total - transacao.valor
        break;
      case TipoTransacoesEnum.Credito:
        valorTotalDescontado = total + transacao.valor
        break;
      case TipoTransacoesEnum.Recebimento_Emprestimo:
        valorTotalDescontado = total + transacao.valor
        break;
      case TipoTransacoesEnum.Vendas:
        valorTotalDescontado = total + transacao.valor
        break;
      case TipoTransacoesEnum.Recebimento_TED:
        valorTotalDescontado = total + transacao.valor
        break;
      case TipoTransacoesEnum.Recebimento_DOC:
        valorTotalDescontado = total + transacao.valor
        break;
      case TipoTransacoesEnum.Aluguel:
        valorTotalDescontado = total - transacao.valor
        break;
      default:
        break;
    }
    return valorTotalDescontado;
  }


  openModal(transacao: ILojaTransacoes): void {
    this.selectedTransacao = transacao;
    this.subTableDataSource.data = transacao.transacoesFinanceira;
    this.dialog.open(this.modalTemplate, {
      maxWidth: '100%',
      height: '90vh',
      width: '95vw'
    }).afterClosed().subscribe(() => {
      this.subTableDataSource = new MatTableDataSource();
      this.filtroTipoModal = '';
    })
  }

  closeModal(): void {
    this.selectedTransacao = null;
    this.dialog.closeAll();
  }
}
