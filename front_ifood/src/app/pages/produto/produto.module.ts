import { NgModule } from "@angular/core";
import { ThemeModule } from "../../@theme/theme.module";
import { ToasterModule } from "angular2-toaster";
import { ProdutoRoutingModule, routedComponents } from "./produto-routing.module";
import { ProdutoService } from "../../@core/data/produto.service";
import { NgSelectModule } from "@ng-select/ng-select";
import { NgxMaskModule } from "ngx-mask";
import { LightboxModule } from "ngx-lightbox";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

@NgModule({
  imports: [ThemeModule,
    ProdutoRoutingModule,
    LightboxModule,
    NgbModule,
    ToasterModule.forRoot(),
    NgSelectModule,
    NgxMaskModule.forRoot(),],
  declarations: [...routedComponents],
  providers: [ProdutoService]
})
export class ProdutoModule {}
