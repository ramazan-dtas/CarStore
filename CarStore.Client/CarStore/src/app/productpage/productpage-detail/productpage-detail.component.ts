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

  constructor(private route: ActivatedRoute,
    private location: Location,
    private productService: ProductService) { }

  ngOnInit(): void {
  }

}
