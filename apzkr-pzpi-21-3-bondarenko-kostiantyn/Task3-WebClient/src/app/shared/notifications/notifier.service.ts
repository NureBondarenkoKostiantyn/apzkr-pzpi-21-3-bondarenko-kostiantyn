import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NotifierComponent } from './notifier.component';

@Injectable({
  providedIn: 'root'
})
export class NotifierService {

  constructor(private snackbar: MatSnackBar) {}

  showSuccessNotification(message: string) {
    this.snackbar.openFromComponent(NotifierComponent, {
      data: {
        message: message
      },
      duration: 3000,
      panelClass: ['success']
    });
  }

  showErrorNotification(message: string){
    this.snackbar.openFromComponent(NotifierComponent, {
      data: {
        message: message
      },
      duration: 3000,
      panelClass: ['error']
    })
  }
}
