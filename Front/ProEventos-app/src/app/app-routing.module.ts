import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  CadastroComponent,
  LoginComponent,
  PerfilComponent,
  UsuarioComponent,
} from './components/usuario';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import {
  EventoDetalheComponent,
  EventoListaComponent,
  EventosComponent,
} from './components/eventos';
import { AuthGuard } from './services/security/guard/auth.guard';
import { HomePageComponent } from './components/home';
import { PalestranteListaComponent, PalestrantesComponent } from './components/palestrantes';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {

    path: 'usuarios',
    component: UsuarioComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'cadastro', component: CadastroComponent },
      { path: 'perfil', component: PerfilComponent },
    ],
  },

  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'eventos', redirectTo: 'eventos/lista' },
      {
        path: 'eventos',
        component: EventosComponent,
        children: [
          { path: 'detalhe/:id', component: EventoDetalheComponent },
          { path: 'detalhe', component: EventoDetalheComponent },
          { path: 'lista', component: EventoListaComponent },
        ],
      },

      { path: 'palestrantes', redirectTo: 'palestrantes/lista' },
      {
        path: 'palestrantes',
        component: PalestrantesComponent,
        children: [
          { path: 'lista', component: PalestranteListaComponent },
        ],
      },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'contatos', component: ContatosComponent },

    ]
  },

  { path: 'home', component: HomePageComponent },


  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
