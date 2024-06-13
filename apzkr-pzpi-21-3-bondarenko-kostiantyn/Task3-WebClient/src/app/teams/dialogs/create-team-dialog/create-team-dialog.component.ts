import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TeamsService } from '../../team.service';
import { Sport } from 'src/app/sports/models/sport';
import { SportService } from 'src/app/sports/sport.service';

@Component({
  selector: 'app-create-team-dialog',
  templateUrl: './create-team-dialog.component.html',
  styleUrls: ['./create-team-dialog.component.scss']
})
export class CreateTeamDialogComponent implements OnInit {
  teamForm!: UntypedFormGroup;
  sports: Sport[] = [];

  constructor(
    private fb: UntypedFormBuilder,
    public dialogRef: MatDialogRef<CreateTeamDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private teamService: TeamsService,
    private sportService: SportService
  ) {}

  ngOnInit(): void {
    this.createForm();
    this.getSports();
  }

  onClose(){
    this.dialogRef.close();
  }

  createForm(){
    this.teamForm = this.fb.group({
      name: [null, Validators.required],
      description: [null],
      countryName: [null, Validators.required],
      sportId: [null, Validators.required]
    })
  }

  onSubmitForm(){
    console.log(this.teamForm);
    this.teamService.createTeam(this.teamForm.value).subscribe(x => {
      this.dialogRef.close();
    })
  }

  getSports(){
    this.sportService.getSports().subscribe(x => {
      this.sports = x;
    })
  }
}
