import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TeamsService } from '../../team.service';

@Component({
  selector: 'app-edit-team-dialog',
  templateUrl: './edit-team-dialog.component.html',
  styleUrls: ['./edit-team-dialog.component.scss']
})
export class EditTeamDialogComponent implements OnInit {
  teamForm!: UntypedFormGroup;

  constructor(
    private fb: UntypedFormBuilder,
    public dialogRef: MatDialogRef<EditTeamDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private teamService: TeamsService
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  onClose(){
    this.dialogRef.close();
  }

  createForm(){
    this.teamForm = this.fb.group({
      name: [this.data.team.name, Validators.required],
      description: [this.data.team.description],
      countryName: [this.data.team.countryName, Validators.required]
    })
  }

  onSubmitForm(){
    this.teamService.editTeam(this.data.team.id, this.teamForm.value).subscribe(x => {
      this.dialogRef.close();
    })
  }
}
