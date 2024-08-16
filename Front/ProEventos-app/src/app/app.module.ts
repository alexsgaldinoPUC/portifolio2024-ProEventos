import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastrModule } from 'ngx-toastr';

import { CurrencyMaskModule } from 'ng2-currency-mask';

import { DateTimeFormatPipe } from './util/pipes';

import { BarraTituloComponent, NavComponent } from './components/shared';
import {
  CadastroComponent,
  LoginComponent,
  PerfilComponent,
  PerfilDetalheComponent,
  UsuarioComponent,
} from './components/usuario';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import {
  EventoDetalheComponent,
  EventoListaComponent,
  EventosComponent,
} from './components/eventos';
import { HomePageComponent } from './components/home';

import {
  AccountService,
  EventoService,
  jwtInterceptor,
  LoteService,
  PalestranteService,
  RedeSocialService,
  UploadService,
} from './services';
import {
  PalestranteDetalheComponent,
  PalestranteListaComponent,
  PalestrantesComponent,
} from './components/palestrantes';
import { RedesSociaisComponent } from './components/redes-sociais';

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
    HomePageComponent,
    LoginComponent,
    PalestrantesComponent,
    PalestranteDetalheComponent,
    PalestranteListaComponent,
    PerfilComponent,
    PerfilDetalheComponent,
    NavComponent,
    RedesSociaisComponent,
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
    PaginationModule.forRoot(),
    ReactiveFormsModule,
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
      progressAnimation: 'increasing',
    }),
    TooltipModule.forRoot(),
  ],
  providers: [
    AccountService,
    EventoService,
    LoteService,
    PalestranteService,
    RedeSocialService,
    UploadService,
    provideHttpClient(withInterceptors([jwtInterceptor])),
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
