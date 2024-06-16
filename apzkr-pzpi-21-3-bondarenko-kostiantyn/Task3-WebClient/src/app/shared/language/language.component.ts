import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-language',
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.scss']
})
export class LanguageComponent implements OnInit {
  currentLanguage!: string;

  constructor(private translate: TranslateService) {
    translate.addLangs(['en', 'ua']);
    translate.setDefaultLang('en');

    translate.use('en');
    this.currentLanguage = 'en';
  }

  switchLanguage(language: string) {
    console.log('lang: ' + language);
    this.currentLanguage = language;
    this.translate.use(language);
  }

  ngOnInit(): void {
  }
}
