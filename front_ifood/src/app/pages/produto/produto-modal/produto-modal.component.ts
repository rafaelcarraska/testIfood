
import { HttpClient, HttpRequest, HttpEventType } from "@angular/common/http";
import { Component, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { ProdutoComponent } from "../produto.component";
import { ProdutoService } from "./../../../@core/data/produto.service";
import { Lightbox, IAlbum } from 'ngx-lightbox';
import { environment } from "../../../../environments/environment";

@Component({
  moduleId: module.id,
  selector: "./produto-modal",
  templateUrl: "./produto-modal.component.html",
  styleUrls: [
    "../../../@theme/styles/modal.scss",
    "./produto-modal.component.scss"
  ]
})
export class ProdutoModalComponent {
  @Input()
  modalHeader: string = "";
  master: boolean;
  produto: ProdutoComponent = new ProdutoComponent();
  listaProdutos: ProdutoComponent[] = [];

  produtoservice: ProdutoService;

  errors: string[];
  erroDescricao: string;
  erroValor: string;

  public albums: Array<any>;
  public progress: number;

  constructor(
    private activeModal: NgbActiveModal,
    produtoservice: ProdutoService,
    private http: HttpClient,
    private _lightbox: Lightbox
  ) {
    this.produtoservice = produtoservice;
  }

  ngAfterViewInit() {
    this.LoadAnexos();
  }

  open(index: number, usaLightbox: boolean): void {
    if (usaLightbox) {
      this._lightbox.open(this.albums.filter(x => x.usaLightbox == true), index);
    }
  }

  funcaoAnexo(extensao: string) {
    switch (extensao) {
      case ".png": return "fa-file-image";
      case ".jpg": return "fa-file-image";
      case ".jpeg": return "fa-file-image";
    }

    return "fa-file";
  }

  usaLightbox(extensao: string) {
    switch (extensao) {
      case ".png": return true;
      case ".jpg": return true;
      case ".jpeg": return true;
    }
    return false;
  }

  LoadAnexos() {
    this.albums = [];
    let album = {
      src: `${environment.serviceUrlFile}assets/images/produtos/${this.produto.imagem}`,
      caption: this.produto.imagem,
      thumb: `${environment.serviceUrlFile}assets/images/produtos/${this.produto.imagem}`,
      Id: this.produto.imagem,
      descricao: this.produto.imagem,
      usaLightbox: true
    };
    this.albums.push(album);


  }

  upload(files) {
    if (files.length === 0) return;

    const formData = new FormData();

    for (let file of files) formData.append(this.produto.idProduto, file);

    const req = new HttpRequest(
      "POST",
      environment.serviceUrl + `Upload/Anexo`,
      formData,
      {
        reportProgress: true
      }
    );

    this.http.request(req).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress) {
        this.progress = Math.round((100 * event.loaded) / event.total);
      }
      else if (event.type === HttpEventType.Response) {
        // this.anexo.arquivo = event.body["id"];
        if (event.body["erro"]) {
          this.ShowErrors(event.body["erro"]);
        } else {
          this.produtoservice.showToast(
            "success",
            "Upload realizado com sucesso!"
          );
        }
        this.LoadAnexos();
      }
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  Salvar() {
    this.LimparErros();
    this.produtoservice.salva(this.produto).subscribe(
      msg => {
        if (msg.sucesso) {
          if (!this.produto.idProduto && msg.content) {
            this.produto.idProduto = msg.content;
            this.listaProdutos.push(this.produto);
          }
          this.produtoservice.showToast(
            "success",
            "produto salvo com sucesso!"
          );
          this.closeModal();
        } else {
          this.ShowErrors(msg.erro);
        }
      },
      erro => {
        if (erro.status === 400) {
          this.InputErros(erro.error);
        }
        if (erro.status === 401 || erro.status === 403) {
          this.produtoservice.showToast("warning", "Aceso negado.");
        }
        this.produtoservice.showToast(
          "error",
          "Não foi possível salvar o produto!"
        );
      }
    );
  }

  LimparErros() {
    this.errors = [];
    this.erroDescricao = "";
  }

  InputErros(validationErrorDictionary) {
    for (var fieldName in validationErrorDictionary) {
      if (validationErrorDictionary.hasOwnProperty(fieldName)) {
        switch (fieldName) {
          case "descricao":
            this.erroDescricao = validationErrorDictionary[fieldName];
            break;
          case "valor":
            this.erroValor = validationErrorDictionary[fieldName];
            break;
          default:
            this.produtoservice.showToast("error", validationErrorDictionary[fieldName]);
            break;
        }
      }
    }
  }

  ShowErrors(errors) {
    errors.forEach(erro => {
      this.produtoservice.showToast("error", erro);
    });
  }
}
