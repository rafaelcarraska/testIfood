
import { Component } from '@angular/core';
import { AnexoComponent } from '../../@core/model/anexos/anexos.component';

@Component({
  moduleId: module.id,
  selector: 'produtos',
  template: `<router-outlet></router-outlet>`,
})
export class ProdutoComponent {
  idProduto: string;
  imagem: string;
  descricao: string;
  valor: number;
}
