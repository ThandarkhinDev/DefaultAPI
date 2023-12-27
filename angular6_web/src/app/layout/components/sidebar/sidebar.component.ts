import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { JwtService } from '../../../core';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
    isActive: boolean = false;
    showMenu: string = '';
    pushRightClass: string = 'push-right';
    menu = []; 
    constructor(private translate: TranslateService, public router: Router, private jwtService: JwtService) {
        this.translate.addLangs(['en', 'fr', 'ur', 'es', 'it', 'fa', 'de', 'myanmar']);
        this.translate.setDefaultLang('en');
        const browserLang = this.translate.getBrowserLang();
        this.translate.use(browserLang.match(/en|fr|ur|es|it|fa|de|myanmar/) ? browserLang : 'en');

        this.router.events.subscribe(val => {
            if (
                val instanceof NavigationEnd &&
                window.innerWidth <= 992 &&
                this.isToggled()
            ) {
                this.toggleSidebar();
            }
        });
        const menulist = JSON.parse(localStorage.getItem('menuList'));
        if (menulist) {
            this.menu = this.createMenuTree(menulist, 0);
        }
    }

    eventCalled() {
        this.isActive = !this.isActive;
    }

    addExpandClass(element: any, menu_ui_sref: any) {
        if (element === this.showMenu) {
            this.showMenu = '0';
        } else {
            if (menu_ui_sref != '#') {
                this.router.navigate([menu_ui_sref]);
            } else {
                this.showMenu = element;
            }
        }
    }

    isToggled(): boolean {
        const dom: Element = document.querySelector('body');
        return dom.classList.contains(this.pushRightClass);
    }

    toggleSidebar() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle(this.pushRightClass);
    }

    rltAndLtr() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle('rtl');
    }

    changeLang(language: string) {
        this.translate.use(language);
    }

    onLoggedout() {
        this.jwtService.destroyToken();
        localStorage.removeItem('isLoggedin');
    }

    // json filter 
    filterJson(jsonobj: any, field: string, value: number  ) {
        return jsonobj.filter(s => s[field] == value);
    }
    // set menu
    createMenuTree(db_data, ParentID) {
        let nodes, node_item, menu_id; 
        // let sub_nodes = ''  , 
        let menu_ui_sref_text, label_text, parent_id, tree_icon_text, permission;
        const i = 0;
        const nodeObj = [];
        nodes = this.filterJson(db_data, 'ParentID', ParentID);
        for (let i = 0; i < nodes.length; i++) {
            if (nodes.length > 0) {
                node_item = nodes[i];
                menu_id = node_item['MenuID'];
                menu_ui_sref_text = node_item['ControllerName'];
                label_text = node_item['MenuName'];
                parent_id = node_item['ParentID'];
                tree_icon_text = node_item['Icon'];
                permission = node_item['Permission'];

                nodeObj.push({ 
                    label: label_text, menu_ui_sref: menu_ui_sref_text,
                    tree_icon: tree_icon_text, menu_permission: permission,
                    menu_parent: parent_id 
                });
                nodeObj[i].children = this.createMenuTree(db_data, menu_id);
            }
        }
        return nodeObj;
    }
}
