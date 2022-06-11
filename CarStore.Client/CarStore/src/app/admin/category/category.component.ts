import { Component, OnInit } from '@angular/core';
import { category } from 'src/app/_model/category';
import { CategoryService } from 'src/app/_service/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  public categorys : category[] = [];
  
  public createCategory : category = { id: 0, categoryName: ''}
  public updateCategory : category = { id: 0, categoryName: ''}

  public getId : number = 0;
  public updateId : number = 0;
  public deleteId : number = 0
  constructor(private categoryService:CategoryService) { }

  ngOnInit(): void {
    setInterval(()=> { this.getAll(false) }, 1000);
  }
  getAll(log : boolean) : void {
    this.categoryService.getAllCategory().subscribe(u=> {
      this.categorys = u
      if (log) console.log(this.categorys[0])
    })
  }
  // ---------------------- Create Category ---------------------- -->
  create(createCategory: category) : void {
    this.categoryService.createCategory(createCategory).subscribe(c =>
      {
        this.createCategory  =  {
          id: 0,
          categoryName: ''
        }
        console.log(c);
      })
  }
  // ---------------------- Update Category ---------------------- -->
  update(id : number, updateCategory : category) : void {
    this.categoryService.updateCategory(id ,updateCategory).subscribe(u => {
      this.updateCategory = u
      console.log(this.updateCategory);
      this.updateId = 0;
      this.updateCategory  =  {
        id: 0,
        categoryName: ''
      }
    })
  }
  // ---------------------- Delete Category ---------------------- -->
  delete(id : number) : void {
    this.categoryService.deleteCategory(id).subscribe(d => {this.updateId = 0; this.deleteId = 0;})
  }

}
