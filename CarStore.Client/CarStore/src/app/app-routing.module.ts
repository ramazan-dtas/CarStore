import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewuserpageComponent } from './newuserpage/newuserpage.component';
import { AdminComponent } from './admin/admin.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { ProductpageComponent } from './productpage/productpage.component';
import { ProfilpageComponent } from './profilpage/profilpage.component';
import { SearchpageComponent } from './searchpage/searchpage.component';
import { ProductpageDetailComponent } from './productpage/productpage-detail/productpage-detail.component';

const routes: Routes = [
  { path: 'newuser', component: NewuserpageComponent },
  { path: 'frontpage', component: FrontpageComponent},
  { path:'adminpage', component: AdminComponent},
  { path:'loginpage', component: LoginpageComponent},
  {path:'productpage', component: ProductpageComponent},
  {path:'profilpage', component: ProfilpageComponent},
  {path:'searchpage', component: SearchpageComponent},
  {path:'productDetailpage', component: ProductpageDetailComponent},
  {path: "", component: FrontpageComponent},

  { path: 'products',component:ProductpageComponent},
  { path: 'products/category',component:ProductpageComponent},
  { path: 'products/category/:category_id',component:ProductpageComponent},
  { path: 'products/:id',component:ProductpageDetailComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }