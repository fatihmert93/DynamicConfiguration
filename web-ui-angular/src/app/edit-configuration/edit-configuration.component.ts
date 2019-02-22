import { Component, OnInit } from '@angular/core';
import {ConfigurationService} from "../service/configuration.service";
import {Router} from "@angular/router";
import {User} from "../model/user.model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {first} from "rxjs/operators";
import { Configuration } from '../model/configuration.model';

@Component({
  selector: 'app-edit-configuration',
  templateUrl: './edit-configuration.component.html',
  styleUrls: ['./edit-configuration.component.css']
})
export class EditConfigurationComponent implements OnInit {

  config: Configuration;
  editForm: FormGroup;
  constructor(private formBuilder: FormBuilder,private router: Router, private configService: ConfigurationService) { }

  ngOnInit() {
    let config =  JSON.parse(localStorage.getItem("editConfig"));
    if(!config) {
      alert("Invalid action.")
      this.router.navigate(['list-configuration']);
      return;
    }
    this.config = config;
  }

  onSubmit() {
    this.configService.update(this.config).subscribe(res => {
      this.router.navigate(['list-configuration']);
    })
  }

}
