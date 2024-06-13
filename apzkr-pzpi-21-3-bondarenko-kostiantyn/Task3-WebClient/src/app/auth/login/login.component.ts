import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { BehaviorSubject, finalize } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: UntypedFormGroup;
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(
    private fb: UntypedFormBuilder,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(){
    this.loginForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, Validators.required]
    })
  }

  onSubmit(){
    if (this.loginForm.invalid){
      return;
    }

    this.loading$.next(true);

    this.authService.login(this.loginForm.value)
      .pipe(finalize(() => {
        this.loading$.next(false);
      }))
      .subscribe({
        next: x => this.router.navigateByUrl('/items'),
        error: err => console.log(err),
        complete: () => this.loading$.next(false)
      });
  }
}
