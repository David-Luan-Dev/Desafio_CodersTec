import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { ILojaTransacoes } from '../Interface/ILojaTransacoes';

@Injectable({
  providedIn: 'root',
})
export class MovimentacoesFinanceiraService extends BaseApiService {

  public getAllMovimentacoes() {
    return this.getAll<ILojaTransacoes>("GetAllTransacoes")
  }

  public uploadFileCNAB(file: File) {
    return this.uploadFile<number>(file, "ArquivoCNAB")
  }
}
