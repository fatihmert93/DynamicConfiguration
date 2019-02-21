import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {AddConfigurationComponent} from "./add-configuration/add-configuration.component";
import {ListConfigurationComponent} from "./list-configuration/list-configuration.component";
import {EditConfigurationComponent} from "./edit-configuration/edit-configuration.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'add-configuration', component: AddConfigurationComponent },
  { path: 'list-configuration', component: ListConfigurationComponent },
  { path: 'edit-configuration', component: EditConfigurationComponent },
  {path : '', component : LoginComponent}
];

export const routing = RouterModule.forRoot(routes);
