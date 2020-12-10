import { ToasterConfig } from "angular2-toaster";

export const environment = {
  production: true,
  serviceUrl:"http://localhost:5000/",
  serviceUrlFile:"http://localhost:5000/",
  usuarioId:"",
  master: false,
  beta: true,
  config: new ToasterConfig({
    positionClass: "toast-top-right",
    timeout: 6000,
    newestOnTop: true,
    tapToDismiss: true,
    preventDuplicates: true,
    animation: "flyLeft",
    limit: 5
  }),
};
