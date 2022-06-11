import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewuserpageComponent } from './newuserpage/newuserpage.component';
//import { AdminComponent } from './admin/admin.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { ProductpageComponent } from './productpage/productpage.component';
import { ProfilpageComponent } from './profilpage/profilpage.component';
import { SearchpageComponent } from './searchpage/searchpage.component';
import { ProductpageDetailComponent } from './productpage/productpage-detail/productpage-detail.component';
import { AdminComponent } from './admin/admin.component';
import { CategoryComponent } from './admin/category/category.component';
import { ProductComponent } from './admin/product/product.component';
import { UserComponent } from './admin/user/user.component';

const routes: Routes = [
  {path: 'category', component: CategoryComponent},
  {path: 'product', component: ProductComponent},
  {path: 'user', component: UserComponent},
  {path: 'admin', component: AdminComponent},
  { path: 'newuser', component: NewuserpageComponent },
  { path: 'frontpage', component: FrontpageComponent},
  //{ path:'adminpage', component: AdminComponent},
  { path:'loginpage', component: LoginpageComponent},
  {path:'productpage', component: ProductpageComponent},
  {path:'profilpage', component: ProfilpageComponent},
  {path:'searchpage', component: SearchpageComponent},
  {path:'productDetailpage', component: ProductpageDetailComponent},
  {path: "", component: FrontpageComponent},

  { path: 'products',component:ProductpageComponent},
  { path: 'products/category/',component:ProductpageComponent},
  { path: 'products/category/:category_id',component:ProductpageComponent},
  { path: 'products/:id',component:ProductpageDetailComponent},
  { path: 'products/category/:category_id/:id', component:ProductpageDetailComponent},
  { path: 'frontpage/:id',component:ProductpageDetailComponent},
  { path: 'productpage/:id',component:ProductpageDetailComponent}
 // { path: 'products/category/:category_id',component:ProductpageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }