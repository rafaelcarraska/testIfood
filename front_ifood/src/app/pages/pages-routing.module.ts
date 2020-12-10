import { AuthenticationGuard } from './../app.guard';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { PagesComponent } from './pages.component';
import { NotFoundComponent } from './miscellaneous/not-found/not-found.component';

const routes: Routes = [{
  path: '',
  component: PagesComponent,
  children: [{
    path: 'home',
    loadChildren: './home/home.module#HomeModule',
    canActivate: [AuthenticationGuard],
  },{
    path: 'produto',
    loadChildren: './produto/produto.module#ProdutoModule',
    canActivate: [AuthenticationGuard],
  },{
    path: '',
    redirectTo: 'home-dashboard',
    pathMatch: 'full',
  }, {
    path: '**',
    component: NotFoundComponent,
  }],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
