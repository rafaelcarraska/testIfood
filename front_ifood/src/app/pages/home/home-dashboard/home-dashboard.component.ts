import { Component } from "@angular/core";
import { NgbModal, NgbCarouselConfig } from "@ng-bootstrap/ng-bootstrap";
import { ToasterConfig } from "angular2-toaster";
import "style-loader!angular2-toaster/toaster.css";
import { ProdutoModalComponent } from "../../produto/produto-modal/produto-modal.component";
import { ProdutoComponent } from "../../produto/produto.component";
import { ProdutoService } from "../../../@core/data/produto.service";
import { PagerService } from "../../../@core/data/pager.service";

@Component({
  selector: "home-dashboard",
  templateUrl: "./home-dashboard.component.html",
  styleUrls: [
    "../../../@theme/styles/grid.scss",
    "./home-dashboard.component.scss"
  ]
})
export class HomeDashboardComponent {
  listaProdutos: ProdutoComponent[] = [];
  produtoService: ProdutoService;
  pagerservice: PagerService;
  public searchString: string;

  constructor(produtoService: ProdutoService,
    pagerservice: PagerService,
    private modalService: NgbModal,
    config: NgbCarouselConfig) {
    this.produtoService = produtoService;
    this.pagerservice = pagerservice;

    config.interval = 3000;
    config.wrap = false;
    config.keyboard = true;
    config.pauseOnHover = true;

      this.LoadGrid();
  }
  config: ToasterConfig;

  pager: any = {};
  pagedItems: any[];
  filtroItems: any[];
  pageSize: number = 20;

  LoadGrid() {
    this.produtoService.lista().subscribe(
      listaProdutos => {
        if (!listaProdutos)
          this.produtoService.showToast("warning", "Nenhum Produto localizado.");

        this.listaProdutos = listaProdutos.content;
        this.setfiltro();
      },
      erro =>  {
        this.produtoService.erro(erro);
      }
    );
  }

  setfiltro() {
    let filter: any = {
      descricao: this.searchString,
      insertUsuarioNome: this.searchString
    };

    let filterKeys = Object.keys(filter);

    this.filtroItems = this.listaProdutos.filter(item => {
      return filterKeys.some(keyName => {
        return (
          new RegExp(filter[keyName], "gi").test(item[keyName]) || filter[keyName] == "");
      });
    });
    this.pagedItems = this.filtroItems;
    this.pager = this.pagerservice.getPager(this.pagedItems.length, 1, this.pageSize);
    this.pagedItems = this.pagedItems.slice(this.pager.startIndex, this.pager.endIndex + 1);

    if (!this.searchString || !Array.isArray(this.listaProdutos)) {
      this.setPage(1);
      return;
    }
  }

  setPage(page: number, orderBy: string = '') {
    if (page < 1 || page > this.pager.totalPages) {
        return;
    }

    // pega o objeto pager do serviço
    if(this.pageSize <= 0){
      this.pageSize = 10;
    }
    if (this.searchString) {
      this.pager = this.pagerservice.getPager(this.filtroItems.length, page, this.pageSize);
      this.pagedItems = this.filtroItems.slice(this.pager.startIndex, this.pager.endIndex + 1);
    }else{
      this.pager = this.pagerservice.getPager(this.listaProdutos.length, page, this.pageSize);
      this.pagedItems = this.listaProdutos.slice(this.pager.startIndex, this.pager.endIndex + 1);
    }
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
  }  

  ShowErrors(errors) {
    if (errors) {
      errors.forEach(erro => {
        this.produtoService.showToast("warning", erro);
      });
    }
  }
}
