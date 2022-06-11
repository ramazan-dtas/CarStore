import { Component, OnInit } from '@angular/core';
import { product } from 'src/app/_model/product';
import { ProductService } from 'src/app/_service/product.service';
import { category } from 'src/app/_model/category';
import { CategoryService } from 'src/app/_service/category.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  public products : product[] = [];
  
  categories: category[] = [];
  category: category = { id: 0, categoryName: ''};

  public getByIdProduct : product = { id: 0, productName: '', price: 0, productionYear: 0, km: 0, description: ''}
  public createProduct : product = { id: 0, productName: '', price: 0, productionYear: 0, km: 0, description: ''}
  public updateProduct : product = { id: 0, productName: '', price: 0, productionYear: 0, km: 0, description: '',}

  public getId : number = 0;
  public updateId : number = 0;
  public deleteId : number = 0
  
  constructor(private productService:ProductService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    setInterval(()=> { this.getAll(false) }, 1000);
    this.categoryService.getAllCategory()
    this.categoryService.getAllCategory().subscribe(x => this.categories = x);
  }
  getAll(log : boolean) : void {
    this.productService.getAllProduct().subscribe(u=> {
      this.products = u
      if (log) console.log(this.products[0])
    })
  }
  getById(id : number) : void {
    this.productService.getById(id).subscribe(
      u=> {
        console.log(u);
        this.getByIdProduct = u;
      }
    )
  }
  create(createProduct: product) : void {
    this.productService.create(createProduct).subscribe(c =>
      {
        this.createProduct  =  {
          id: 0,
          productName: '',
          price: 0,
          productionYear: 0,
          km: 0,
          description: '',
        }
        console.log(c);
      })
  }
  update(id : number, updateProduct : product) : void {
    this.productService.update(id ,updateProduct).subscribe(u => {
      this.updateProduct = u
      console.log(this.updateProduct);
      this.updateId = 0;
      this.updateProduct  =  {
        id: 0,
        productName: '',
        price: 0,
        productionYear: 0,
        km: 0,
        description: '',
      }
    })
  }
  delete(id : number) : void {
    this.productService.delete(id).subscribe(d => {this.updateId = 0; this.deleteId = 0;})
  }

}
