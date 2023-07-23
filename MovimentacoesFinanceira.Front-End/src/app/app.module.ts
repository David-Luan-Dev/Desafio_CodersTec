import { DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip'; // Import do MatTooltipModule
import { MatIconModule } from '@angular/material/icon'; // Import do MatIconModule
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import localePt from '@angular/common/locales/pt';
registerLocaleData(localePt)



import { AppComponent } from './app.component';
import { TelaMovimentacaoFinanceiraComponent } from './Pages/tela-movimentacao-financeira/tela-movimentacao-financeira.component';
import { AppRoutingModule } from './app-routing.module';
import { CPFPipe } from './shared/pipe/cpf.pipe';
import { registerLocaleData } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    TelaMovimentacaoFinanceiraComponent,
    CPFPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatTableModule,
    MatButtonModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatIconModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    MatDialogModule
  ],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt-BR'
    },

    /* if you don't provide the currency symbol in the pipe,
    this is going to be the default symbol (R$) ... */
    {
      provide: DEFAULT_CURRENCY_CODE,
      useValue: 'BRL'
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
