import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelaMovimentacaoFinanceiraComponent } from './tela-movimentacao-financeira.component';

describe('TelaMovimentacaoFinanceiraComponent', () => {
  let component: TelaMovimentacaoFinanceiraComponent;
  let fixture: ComponentFixture<TelaMovimentacaoFinanceiraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TelaMovimentacaoFinanceiraComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TelaMovimentacaoFinanceiraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
