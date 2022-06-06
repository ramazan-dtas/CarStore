import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewuserpageComponent } from './newuserpage/newuserpage.component';

const routes: Routes = [
  { path: 'newuser', component: NewuserpageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }