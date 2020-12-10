import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutoComponent } from './produto.component';
import { ProdutoGridComponent } from './produto-grid/produto-grid.component';
import { ProdutoModalComponent } from './produto-modal/produto-modal.component';

const routes: Routes = [{
  path: '',
  component: ProdutoComponent,
  children: [{
    path: 'produto-grid',
    component: ProdutoGridComponent,
  }, {
    path: 'modals',
    component: ProdutoModalComponent,
  }]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProdutoRoutingModule { }

export const routedComponents = [
  ProdutoComponent,
  ProdutoGridComponent,
  ProdutoModalComponent,
];
