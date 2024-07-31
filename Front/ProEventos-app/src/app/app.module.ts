import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastrModule } from 'ngx-toastr';

import { DateTimeFormatPipe } from './util/pipes';

import { BarraTituloComponent } from './components/shared/barraTitulo/barraTitulo.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NavComponent } from './components/shared/nav/nav.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import {
  EventoDetalheComponent,
  EventoListaComponent,
  EventosComponent,
} from './components/eventos';
import {
  CadastroComponent,
  LoginComponent,
  PerfilComponent,
  UsuarioComponent,
} from './components/usuario';
import { EventoService, LoteService } from './services';
import { CurrencyMaskModule } from 'ng2-currency-mask';

defineLocale('pt-br', ptBrLocale);

@NgModule({
  declarations: [
    AppComponent,
    BarraTituloComponent,
    CadastroComponent,
    ContatosComponent,
    DashboardComponent,
    DateTimeFormatPipe,
    EventoDetalheComponent,
    EventoListaComponent,
    EventosComponent,
    LoginComponent,
    PalestrantesComponent,
    PerfilComponent,
    NavComponent,
    UsuarioComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    CurrencyMaskModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    NgxSpinnerModule.forRoot({
      type: 'ball-climbing-dot',
    }),
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
      progressAnimation: 'increasing',
    }),
    TooltipModule.forRoot(),
  ],
  providers: [EventoService, LoteService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
