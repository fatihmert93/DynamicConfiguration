import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {ConfigurationService} from "../service/configuration.service";
import {User} from "../model/user.model";
import { Configuration } from '../model/configuration.model';
import { ResponseModel } from '../model/responseModel';

@Component({
  selector: 'app-list-configuration',
  templateUrl: './list-configuration.component.html',
  styleUrls: ['./list-configuration.component.css']
})
export class ListConfigurationComponent implements OnInit {

  configs: Configuration[] = [];

  searchModel: String = "";

  constructor(private router: Router, private configService: ConfigurationService) { }

  ngOnInit() {
    this.getConfigs();
  }

  getConfigs(){
    this.configService.getList(this.searchModel)
      .subscribe( res => {
        var responseModel = res;
        console.log(res.data);
        this.configs = res.data;
      });
  }

  searchChange(event){
    setTimeout(() => {
      this.getConfigs();
    },500);
    
  }

  delete(config: Configuration): void {
    this.configService.delete(config._id)
      .subscribe( data => {
        this.getConfigs();
      })
  };

  edit(config: Configuration): void {
    localStorage.removeItem("editUserId");
    localStorage.setItem("editUserId", config._id);
    this.router.navigate(['edit-configuration']);
  };

  addConfiguration(): void {
    this.router.navigate(['add-configuration']);
  };
}
