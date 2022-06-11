import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { product } from 'src/app/_model/product';
import { ProductService } from 'src/app/_service/product.service';

@Component({
  selector: 'app-productpage-detail',
  templateUrl: './productpage-detail.component.html',
  styleUrls: ['./productpage-detail.component.css']
})

export class ProductpageDetailComponent implements OnInit {
  id : number = 0;
  product: product = {
    id: 0,
    productName: "",
    productionYear: 0,
    km: 0,
    description: "",
    categoryId: 1,
    category: {
      id: 0,
      categoryName: ""
    },
    price: 0,
    images: ""
  };

  constructor(private route: ActivatedRoute,
    private location: Location,
    private productService: ProductService) { }

  ngOnInit(): void {
    this.getProduct();
  }
  getProduct(): void {
    this.id = (this.route.snapshot.paramMap.get('id') || 0) as number;
    console.log(this.id);
    this.productService.getProduct(this.id).subscribe(
      product => {
        this.product = product;
        if (this.product == null) {
          this.location.go("/products")
        }
      }
    )
  }

}
