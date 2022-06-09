import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { category } from '../_model/category';
import { product } from '../_model/product';
import { stringify } from '@angular/compiler/src/util';
import { ProductService } from '../_service/product.service';
import { CategoryService } from '../_service/category.service';


@Component({
  selector: 'app-productpage',
  templateUrl: './productpage.component.html',
  styleUrls: ['./productpage.component.css']
})
export class ProductpageComponent implements OnInit {
  products: product[] = [];
  categories: category[] = [];

  constructor(private route: ActivatedRoute,
    private location: Location,
    private productService: ProductService,
    private categoryService: CategoryService,) {}

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
  }
  getProducts(): void {
    //checks if it should sort by categoryId
    if( this.location.path().includes ("products/category/") ){
      var categoryId = (this.route.snapshot.paramMap.get('category_id') || 0) as number;
      this.productService.getProductsByCategoryId(categoryId)
        .subscribe(product => this.products = product)
    } else { //else it just gets everything
      this.productService.getAllProduct()
        .subscribe(product => this.products = product)
    }
  }

  getCategories(): void {
    this.categoryService.getAllCategory()
      .subscribe(category => this.categories = category)
  }

}
