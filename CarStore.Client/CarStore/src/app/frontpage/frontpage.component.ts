import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder } from '@angular/forms';
import { category } from '../_model/category';
import { product } from '../_model/product';
import { ProductService } from '../_service/product.service';
import { CategoryService } from '../_service/category.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  id : number = 0;
  products: product[] = [];
  categories: category[] = [];
  product: product[] = [];

  constructor(private route: ActivatedRoute,
    private location: Location,
    private productService : ProductService,) {
    
   }

  ngOnInit(): void {
    this.getProducts();
  }
  getProducts(): void {
    this.id = (this.route.snapshot.paramMap.get('id') || 0) as number;
    console.log(this.id);
    this.productService.getAllProduct()
      .subscribe(products => {
      this.products = products.sort(() => Math.random() - Math.random()).slice(0,3); 
      if (this.product == null) {
        this.location.go("/products") 
      }
    }
  )
  }
}
