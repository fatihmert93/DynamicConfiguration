import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ConfigurationService} from "../service/configuration.service";
import {first} from "rxjs/operators";
import {Router} from "@angular/router";
import { Configuration } from '../model/configuration.model';

@Component({
  selector: 'app-add-configuration',
  templateUrl: './add-configuration.component.html',
  styleUrls: ['./add-configuration.component.css']
})
export class AddConfigurationComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,private router: Router, private configService: ConfigurationService) { }

  config: Configuration = <Configuration> {};

  ngOnInit() {

    

  }

  onSubmit() {
    this.configService.create(this.config)
      .subscribe( data => {
        this.router.navigate(['list-configuration']);
      });
  }

}
