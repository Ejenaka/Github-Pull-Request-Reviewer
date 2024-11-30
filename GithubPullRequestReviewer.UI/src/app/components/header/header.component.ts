import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../api/pull-request/services';
import { User } from '../../api/pull-request/models';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  user: Observable<User>;

  constructor(
    private readonly userApiService: UserService,
    private readonly authService: AuthService) { }

  ngOnInit() {
    this.user = this.userApiService.apiUsersCurrentGet$Json({ access_token: this.authService.getAccessToken() });
  }

  onlogOutClick() {
    this.authService.logOut();
  }
}
