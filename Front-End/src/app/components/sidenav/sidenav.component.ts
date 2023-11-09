import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

interface MenuItem {
  icon: string;
  title: string;
  link: string;
}
@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent {

  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;
  isMobile = true;
  isCollapsed = true;
  admin: boolean = this.authService.isAdmin();

  items: MenuItem[] = [];

  constructor(private observer: BreakpointObserver, private authService: AuthenticationService) {
    this.items = [{
      icon: 'card_giftcard',
      title: "Prodottis",
      link: '/products'
    },
    {
      icon: 'favorite',
      title: "Wishlist",
      link: '/wish-list'
    },
    {
      icon: 'account_circle',
      title: "Profilo",
      link: '/profile'
    },
    {
      icon: 'logout',
      title: "Logout",
      link: '/logout'
    }
    ]
  }

  ngOnInit() {
    this.observer.observe(['(max-width: 800px)']).subscribe((screenSize) => {
      if (screenSize.matches) {
        this.isMobile = true;
      } else {
        this.isMobile = false;
      }
    });
  }

  toggleMenu() {
    if (this.isMobile) {
      this.sidenav.toggle();
      this.isCollapsed = false;
    } else {
      this.sidenav.open();
      this.isCollapsed = !this.isCollapsed;
    }
  }
  
}
