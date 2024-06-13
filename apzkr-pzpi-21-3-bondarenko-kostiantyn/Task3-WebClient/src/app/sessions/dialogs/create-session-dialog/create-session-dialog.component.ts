import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TeamsService } from 'src/app/teams/team.service';
import { SessionService } from '../../session.service';
import { NotifierService } from 'src/app/shared/notifications/notifier.service';

@Component({
  selector: 'app-create-session-dialog',
  templateUrl: './create-session-dialog.component.html',
  styleUrls: ['./create-session-dialog.component.scss']
})
export class CreateSessionDialogComponent implements OnInit {
  sessionForm!: UntypedFormGroup;

  constructor(
    private fb: UntypedFormBuilder,
    public dialogRef: MatDialogRef<CreateSessionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private sessionService: SessionService,
    private notifierService: NotifierService
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  onClose(){
    this.dialogRef.close();
  }

  createForm(){
    this.sessionForm = this.fb.group({
      duration: [null, [Validators.required,Validators.min(10), Validators.max(600)]]
    })
  }

  onSubmitForm(){
    let session = this.sessionForm.value;
    session.teamId = this.data.teamId;
    this.sessionService.createSession(session).subscribe(x => {
      this.dialogRef.close();
      this.notifierService.showSuccessNotification('Successfully created a sessio');
    }, err => {
      console.log(err);
      this.notifierService.showErrorNotification(err.name);
    })
  }
}
