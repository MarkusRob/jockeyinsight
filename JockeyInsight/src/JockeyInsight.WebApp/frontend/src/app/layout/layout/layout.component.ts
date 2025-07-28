import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {NavbarComponent} from "../../shared/navbar/navbar.component";
import {FooterComponent} from "../../shared/footer/footer.component";
import {UploadComponent} from "../../shared/upload/upload.component";

@Component({
  selector: 'layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavbarComponent, FooterComponent, UploadComponent],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent {}
