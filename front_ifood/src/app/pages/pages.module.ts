import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ThemeModule } from '../@theme/theme.module';
import { CustomHttpInterceptor } from '../app.interceptor';
import { MiscellaneousModule } from './miscellaneous/miscellaneous.module';
import { PagesRoutingModule } from './pages-routing.module';
import { PagesComponent } from './pages.component';
import { HomeModule } from './home/home.module';
import { ProdutoModule } from './produto/produto.module';


const PAGES_COMPONENTS = [
  PagesComponent,
];

@NgModule({
  imports: [
    HomeModule,
    PagesRoutingModule,
    ThemeModule,
    MiscellaneousModule,
    ProdutoModule,
  ],
  declarations: [
    ...PAGES_COMPONENTS,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, useClass: CustomHttpInterceptor, multi: true
  }]
})
export class PagesModule {
}
