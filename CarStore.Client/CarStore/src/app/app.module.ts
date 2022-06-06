import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AdminComponent } from './admin/admin.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { NewuserpageComponent } from './newuserpage/newuserpage.component';
import { ProductpageComponent } from './productpage/productpage.component';
import { ProfilpageComponent } from './profilpage/profilpage.component';
import { SearchpageComponent } from './searchpage/searchpage.component';
import { AppRoutingModule } from './app-routing.module';
import { ProductpageDetailComponent } from './productpage/productpage-detail/productpage-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    FrontpageComponent,
    LoginpageComponent,
    NewuserpageComponent,
    ProductpageComponent,
    ProfilpageComponent,
    SearchpageComponent,
    ProductpageDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
