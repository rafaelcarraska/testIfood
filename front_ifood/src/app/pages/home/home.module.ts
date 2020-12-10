
import { NgModule } from "@angular/core";
import { TreeModule } from 'angular-tree-component';
import { HomeRoutingModule, routedComponents } from "./home-routing.module";
import { ThemeModule } from "../../@theme/theme.module";
import { ToasterModule } from "angular2-toaster";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { PagerService } from "../../@core/data/pager.service";

@NgModule({
  imports: [ThemeModule, HomeRoutingModule, NgbModule.forRoot(), TreeModule, ToasterModule.forRoot()],
  declarations: [...routedComponents],
  providers: [PagerService]
})
export class HomeModule {}
