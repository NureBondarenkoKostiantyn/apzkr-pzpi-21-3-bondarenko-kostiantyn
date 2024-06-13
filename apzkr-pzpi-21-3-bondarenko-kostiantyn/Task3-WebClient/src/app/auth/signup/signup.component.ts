import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, finalize } from 'rxjs';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  signupForm!: UntypedFormGroup;
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(
    private fb: UntypedFormBuilder,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(){
    this.signupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    })
  }

  onSubmit(){
    if (this.signupForm.invalid){
      return;
    }

    this.loading$.next(true);

    this.authService.signup(this.signupForm.value)
      .pipe(finalize(() => {
        this.loading$.next(false);
      }))
      .subscribe({
        next: x => this.router.navigateByUrl('/login'),
        error: err => console.log(err),
        complete: () => this.loading$.next(false)
      });
  }
}
