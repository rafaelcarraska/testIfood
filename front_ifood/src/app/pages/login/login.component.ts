import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { LoginService } from "../../@core/data/login.service";
import { AutenticacaoComponent } from "../../@core/model/autenticacao/autenticacao.component";


@Component({
  selector: "login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent {
  loginservice: LoginService;
  autenticacao: AutenticacaoComponent = new AutenticacaoComponent();
  returnLogin: string;

  load: boolean;

  constructor(
    loginservice: LoginService,
    private readonly router: Router,

  ) {
    this.loginservice = loginservice;
  }

  ngAfterViewInit(){
    sessionStorage.clear();
    this.load = false;
    this.Sair();
  }

  Sair() {
    this.loginservice.Sair().subscribe(msg => {
    },erro => {
    });
  }

  Logar() {
    this.load = true;
    this.returnLogin = "Aguarde, estamos validando os dados.";
    this.loginservice.Logar(this.autenticacao).subscribe(
      msg => {
        if (msg.sucesso) {
          this.returnLogin = "";
          this.load = false;
          if (msg.content) {
            sessionStorage.setItem("token", msg.content);
            this.router.navigate(["/pages/home/home-dashboard"]);
          }
        } else {
          this.returnLogin = msg.erro;
          this.load = false;
        }
      },
      erro => {
        this.load = false;
        this.returnLogin = "";
        if (erro.status === 400) {
          this.returnLogin = erro.text();
        }
        this.returnLogin = "Não foi possível realizar login";
      }
    );
  }


}
