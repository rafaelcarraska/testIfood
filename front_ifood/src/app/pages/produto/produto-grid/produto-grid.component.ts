import { Component } from "@angular/core";
import { ProdutoComponent } from "../produto.component";
import { ProdutoService } from "../../../@core/data/produto.service";
import { ProdutoModalComponent } from "../produto-modal/produto-modal.component";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import {
  ToasterConfig
} from "angular2-toaster";

@Component({
  selector: "produto-grid",
  templateUrl: "./produto-grid.component.html",
  styleUrls: [
    "../../../@theme/styles/grid.scss",
    "./produto-grid.component.scss",
  ]
})
export class ProdutoGridComponent {
  listaProdutos: ProdutoComponent[] = [];
  produtoservice: ProdutoService;

  constructor(
    produtoservice: ProdutoService,
    private modalService: NgbModal) {
      this.produtoservice = produtoservice;

      this.LoadGrid();
  }
  config: ToasterConfig;

  LoadGrid() {
    this.produtoservice.lista().subscribe(
      listaProdutos => {
        if (!listaProdutos)
          this.produtoservice.showToast("warning", "Nenhum Produto localizado.");

        this.listaProdutos = listaProdutos.content;
      },
      erro =>  {
        this.produtoservice.erro(erro);
      }
    );
  }

  Novo() {
    const activeModal = this.modalService.open(ProdutoModalComponent, {
      size: "lg",
      backdrop: "static",
      container: "nb-layout"
    });
    let novoProduto = new ProdutoComponent();
    novoProduto.idProduto = "";
    activeModal.componentInstance.modalHeader = "Novo Produto";
    activeModal.componentInstance.produto = novoProduto;
    activeModal.componentInstance.listaProdutos = this.listaProdutos;
  }

  Modal(produto: ProdutoComponent) {
    const activeModal = this.modalService.open(ProdutoModalComponent, {
      size: "lg",
      backdrop: "static",
      container: "nb-layout"
    });

    activeModal.componentInstance.modalHeader =
      "Editar Produto " + produto.descricao;
    activeModal.componentInstance.produto = produto;
    activeModal.componentInstance.listaProdutos = this.listaProdutos;

    // this.router.navigate(["/pages/produto/"+produto.id]);
  }  

  Deletar(produto): void {
    if (window.confirm("Deseja deletar esse Produto?")) {
      this.produtoservice.remove(produto).subscribe(
        msg => {
          if (msg.sucesso) {
            let novoProduto = this.listaProdutos.slice(0);
            let indice = novoProduto.indexOf(produto);
            novoProduto.splice(indice, 1);
            this.listaProdutos = novoProduto;
            this.produtoservice.showToast(
              "success",
              "Produto deletado com sucesso."
            );
          } else {
            this.ShowErrors(msg.erro);
          }
        },
        erro => {
          if (erro.status === 401 || erro.status === 403) {
            this.produtoservice.showToast("warning", "Aceso negado.");
          }else{
            this.produtoservice.showToast("error", "Erro ao deletar o produto.");
          }
        }
      );
    }
  }

  ShowErrors(errors) {
    if (errors) {
      errors.forEach(erro => {
        this.produtoservice.showToast("warning", erro);
      });
    }
  }
}
