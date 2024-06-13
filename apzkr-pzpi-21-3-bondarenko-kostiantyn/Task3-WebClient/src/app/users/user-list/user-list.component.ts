import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';
import { UserService } from '../user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  displayedColumns: string[] = ['email', 'firstName', 'lastName'];

  constructor(
    private userService: UserService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.userService.getUsers().subscribe(x => {
      this.users = x;
      console.log(this.users);
    })
  }

  onNavigateToUser(id: string){
    this.router.navigateByUrl(`/users/${id}`);
  }
}
