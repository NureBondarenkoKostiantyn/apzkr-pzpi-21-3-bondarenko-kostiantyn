import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  show: boolean = true;

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  shouldShowToolbar(): boolean {
    const currentPath = this.router.url;
    return !currentPath.includes('auth/login') && !currentPath.includes('auth/signup');
  }
}
