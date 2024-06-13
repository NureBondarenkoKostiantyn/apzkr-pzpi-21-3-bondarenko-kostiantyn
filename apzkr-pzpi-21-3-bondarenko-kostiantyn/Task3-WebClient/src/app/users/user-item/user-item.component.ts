import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-item',
  templateUrl: './user-item.component.html',
  styleUrls: ['./user-item.component.scss']
})
export class UserItemComponent implements OnInit {
  user!: User;

  constructor(
    private userService: UserService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(x => {
      let id = x['id']
      this.getUserById(id);
    });
  }

  getUserById(id: string){
    this.userService.getUserById(id).subscribe(x => {
      this.user = x;
    })
  }
}
