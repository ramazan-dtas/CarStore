import { Component, OnInit } from '@angular/core';
import { role } from 'src/app/_model/role';
import { user } from 'src/app/_model/user';
import { UserService } from 'src/app/_service/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  public users : user[] = [];
  public user : user = {}
  public id : number = 1; // 
  public newUser : user = {};

  public updateUserId : number = 1;
  public updateUser : user = {}
  public deleteUserId : number = 1


  constructor(private userService: UserService) { }

  ngOnInit(): void {
    setInterval(()=> { this.getAll(false) }, 1000);
  }
  getAll(log : boolean) : void {    
    this.userService.getAll().subscribe(u=> {
      this.users = u
      if (log) console.log(u)
    })
  }


  getById(id : number) : void {
    this.userService.getById(id).subscribe(
      u=> {
        this.user = u;
      }
    )
  }



  create(user: user) : void {
    this.userService.create(user).subscribe(c => {this.newUser = c})
  }

  update(id : number, user : user) : void {
    this.userService.update(id ,user).subscribe(u => {this.updateUser = u})
  }

  delete(id : number) : void {
    this.userService.delete(id).subscribe(
      d => console.log(d)
    )
  }
}