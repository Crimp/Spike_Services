/* 
* DXTREME ENTERPRISE PREVIEW
* Copyright © 2012 Developer Express Inc. ALL RIGHTS RESERVED
* EULA: DevExpress.com/EULA-DXTREME-HTML-CTP
*/
(function(n,t){t.framework={}})(jQuery,DevExpress),function(n,t,i){var r=t.Class;t.framework.Route=r.inherit({_trimSeparators:function(n){return n.replace(/^[/.]+|[/.]+$/g,"")},_escapeRe:function(n){return n.replace(/\W/g,"\\$1")},_checkConstraint:function(n,t){n=String(n),typeof t=="string"&&(t=new RegExp(t));var i=t.exec(n);return!i||i[0]!==n?!1:!0},_ensureReady:function(){var i,r,t=this;if(this._patternRe)return!1;this._pattern=this._trimSeparators(this._pattern),this._patternRe="",this._params=[],this._segments=[],this._separators=[],this._pattern.replace(/[^/.]+/g,function(n,i){t._segments.push(n),i&&t._separators.push(t._pattern.substr(i-1,1))}),n.each(this._segments,function(n){var u=!0,i=this,r=n?t._separators[n-1]:"";i.charAt(0)===":"?(u=!1,i=i.substr(1),t._params.push(i),t._patternRe+="(?:"+r+"([^/.]+))",i in t._defaults&&(t._patternRe+="?")):t._patternRe+=r+t._escapeRe(i)}),this._patternRe=new RegExp("^"+this._patternRe+"$")},ctor:function(n,t,i){this._pattern=n||"",this._defaults=t||{},this._constraints=i||{}},parse:function(t){var r=this,i;return(this._ensureReady(),!this._patternRe.test(t))?!1:(i=n.extend({},this._defaults),n.each(this._params,function(n){i[this]=RegExp["$"+(n+1)]||i[this]}),n.each(this._constraints,function(n){if(!r._checkConstraint(i[n],r._constraints[n]))return i=!1,!1}),i)},format:function(t){var u=this,e;this._ensureReady();var f=n.extend({},this._defaults),o=0,r=[],s=[];return(n.each(this._segments,function(n,e){if(r[n]=n?u._separators[n-1]:"",e.charAt(0)==":"){var h=e.substr(1);if(!(h in t)&&!(h in u._defaults)||h in u._constraints&&!u._checkConstraint(t[h],u._constraints[h]))return r=null,!1;h in t?t[h]!==i&&(f[h]=t[h],r[n]+=t[h],o=n):h in f&&(r[n]+=f[h],s.push(n))}else r[n]+=e,o=n}),n.each(t,function(){if(!this in f)return r=null,!1}),r===null)?!1:(s.length&&n.map(s,function(n){n>=o&&(r[n]="")}),e=r.join(""),e=e.replace(/(.*)\/$/,"$1"))}}),t.framework.MvcRouter=t.Class.inherit({ctor:function(){this._registry=[]},_trimSeparators:function(n){return n.replace(/^[/.]+|[/.]+$/g,"")},_createRoute:function(n,i,r){return new t.framework.Route(n,i,r)},register:function(n,t,i){this._registry.push(this._createRoute(n,t,i))},parse:function(t){var i;return t=this._trimSeparators(t),n.each(this._registry,function(){var n=this.parse(t);if(n!==!1)return i=n,!1}),i?i:!1},format:function(t){var i;return(t=t||{},n.each(this._registry,function(){var n=this.format(t);if(n!==!1)return i=n,!1}),typeof i=="string")?i:!1}})}(jQuery,DevExpress),function(n,t){var r=t.Class;t.framework.Command=r.inherit({ctor:function(n){this.info=n},execute:function(){if(!this.canExecute.apply(this,arguments))throw new Error("Cannot execute command:"+this.info.id);var n=this.info.execute;if(!n)throw new Error("Command handler not found:"+this.info.id);n.apply(this,arguments)},canExecute:function(){var n=!0,t=this.info.canExecute;return t&&(n=this.info.canExecute.apply(this.info,arguments)),n}})}(jQuery,DevExpress),function(n,t,i){var u=t.Class,r;t.framework.BrowserNavigationDevice=u.inherit({ctor:function(){var t=this;t.urichanged=n.Callbacks();n(window).on("hashchange",function(){t.urichanged.fire()})},getUri:function(){return document.location.hash.replace("#","")},setUri:function(n){document.location.hash="#"+n}}),r={current:"current",blank:"blank",back:"back"},t.framework.NavigationManager=u.inherit({_onUriChanged:function(){this.navigate()},ctor:function(r){r=r||{};var u=this;u._history=[],u.currentUri=i,u.navigated=n.Callbacks(),u._navigationDevice=r._navigationDevice||new t.framework.BrowserNavigationDevice,u._navigationDevice.urichanged.add(function(){u._onUriChanged()})},navigate:function(n,t){if(t=t||{},n===i&&(n=this._navigationDevice.getUri()),t.clearHistory&&(this._history.length=0),this.currentUri!==n){var r=this.currentUri;this.currentUri=n,this._navigationDevice.setUri(n),this.navigated.fire({uri:n,previousUri:r,target:t.target||this._predictTarget(n)})}},_predictTarget:function(n){return this._history.length>1&&this._history[this._history.length-2].uri===n?r.back:r.blank},back:function(){var n=this._history.length>1?this._history[this._history.length-2]:i;n&&this.navigate(n.uri,{target:r.back,viewInfo:n})},indexOf:function(t){return n.inArray(t,this._history)},getViewByIndex:function(n){return this._history[n]},push:function(n){this._history.push(n)},addView:function(n,t){t===r.back?this.pop(2):t===r.current&&this.pop(),this.push(n)},getView:function(n){for(var t=this._history.length-1;t>=0;t--)if(this._history[t].uri===n)return this._history[t]},pop:function(n){for(n=n||1;--n;)this._history.pop();return this._history.pop()}}),t.framework.NavigationManager.NAVIGATION_TARGETS=r}(jQuery,DevExpress),function(n,t){var r=t.Class,u=t.framework.NavigationManager.NAVIGATION_TARGETS;t.framework.Application=r.inherit({ctor:function(i){this.ns=i.ns||window,this.router=i.router||new t.framework.MvcRouter,this.navigationManager=i.navigationManager||new t.framework.NavigationManager,this.navigationManager.navigated.add(n.proxy(this._onNavigated,this)),this.beforeViewSetup=n.Callbacks(),this.afterViewSetup=n.Callbacks(),this.viewShowing=n.Callbacks(),this.viewShown=n.Callbacks()},_onNavigated:function(n){if(n.uri=="_back"){this.back();return}var t=this._aquireView(n.uri,n.target);this._setCurrentView(t,n.target)},_aquireView:function(n,t){var i;return i=t===u.back?this.navigationManager.getView(n):this._createView(n)},_createView:function(n){var i=this.router.parse(n),t={viewName:i.view,uri:this.router.format(i)};return this.beforeViewSetup.fire({routeData:i,viewInfo:t}),t.model=t.model||this._createModel(i),this.afterViewSetup.fire(t),t},_createModel:function(n){var t=this.ns[n.view];return t?t.call(this.ns,n):{}},_setCurrentView:function(n,t){var r=this,i={viewInfo:n,target:t},u;if(r.viewShowing.fire(i),u=i.viewInfo.model.viewShowing,u&&u.call(n.model,i),!i.cancel)return r.navigationManager.addView(n,t),r._setCurrentViewAsyncImpl(i.viewInfo,t).done(function(){r.viewShown.fire(i);var t=i.viewInfo.model.viewShown;t&&t.call(n.model,i)})},_setCurrentViewAsyncImpl:DevExpress.abstract,navigate:function(n,t){this.navigationManager.navigate(n,t)},back:function(){this.navigationManager.back()}})}(jQuery,DevExpress),function(n,t){t.framework.html={layoutControllers:{}}}(jQuery,DevExpress),function(n,t){var r=t.framework.html.commandToDXWidgetAdapters={};r.dxToolbar={addCommand:function(n,t){var r={text:t.info.title,click:function(){t.canExecute&&t.execute()}},i;t.info.location==="back"&&(r.type="back"),i=n.data("dxToolbar").option("items"),i.push({align:t.info.location==n.data("leftLocations")?"left":"right",widget:"button",options:r}),n.data("dxToolbar").option("items",i)}},r.dxList={addCommand:function(n,t){var i=n.data("dxList"),r=i.option("items");r.push({title:t.info.title,uri:t.info.uri}),i.option("items",r)}},r.dxNavBar={addCommand:function(n,t){var i=n.data("dxNavBar"),r=i.option("items");r.push({text:t.info.title,uri:"#"+t.info.uri,icon:t.info.icon,iconSrc:t.info.iconSrc}),i.option("items",r)}}}(jQuery,DevExpress),function(n,t,i){var r=t.Class;(function(){ko.bindingHandlers.command={init:function(t,i){n(t).data("commandValueAccessor",i)},update:function(){}}})(),t.framework.html.CommandManager=r.inherit({ctor:function(n){n=n||{},this._globalCommands=n.globalCommands||[],this.navigationManager=n.navigationManager,this.commandToWidgetRegistry=[this._commandToDXWidget]},_commandToDXWidget:function(n,i){var f=i.data("dxComponents"),r,u;return f&&(r=f[0],u=t.framework.html.commandToDXWidgetAdapters,r in u)?(u[r].addCommand(i,n),!0):!1},_createModelHandler:function(n,t){var r=i;return n&&(r=function(){return n.apply(t,arguments)}),r},_getCommandInfo:function(n,t){var u=n.data("id"),f=n.data("location"),e=n.data("title"),i=n.data("commandValueAccessor"),r=i?i():{};return{id:u,execute:this._createModelHandler(r.execute,t),canExecute:this._createModelHandler(r.canExecute,t),location:f,$source:n,title:e}},_createCommands:function(i,r){var f=this,u=[];return i.find("*[data-role='command']").each(function(){var o=n(this),i,e;ko.applyBindings(r,this),i=f._getCommandInfo(o,r),e=new t.framework.Command(i),u.push(e)}),u},_mapCommands:function(t,i){var r=[];return t.find("*[data-command-container]").each(function(){var t=n(this),f=t.data("command-container"),u={$container:t,commands:[]};r.push(u),n.each(f.split(";"),function(t,r){n.each(i,function(n,t){t.info.location===r&&u.commands.push(t)})})}),r},_attachCommands:function(t){var i=this;n.each(t,function(t,r){n.each(r.commands,function(n,t){i._attachCommandToContainer(t,r.$container)})})},_attachCommandToContainer:function(t,i){var r=!1;n.each(this.commandToWidgetRegistry,function(n,u){return r=u(t,i),!r}),r||this._defaultCommandToContainer(t,i)},_defaultCommandToContainer:function(n,t){var i=n.info.$source;if(i){t.append(i);i.on("click",function(){n.execute()})}},layoutCommands:function(n,t,i){i=i||{};var o=this,r=this._createCommands(t,i),u=i._commands||[],f=this._globalCommands.concat(u).concat(r),e=this._mapCommands(n,f);this._attachCommands(e)}})}(jQuery,DevExpress),function(n,t){var e=t.Class,u=t.framework.NavigationManager.NAVIGATION_TARGETS,r="*[data-placeholder]:not(*[data-placeholder] *[data-placeholder])",f=function(n){return"[data-placeholder='"+n+"']"};t.framework.html.DefaultLayoutController=e.inherit({ctor:function(n){this.currentViewIndex=-1,this.init(n||{})},init:function(n){this._navigationManager=n.navigationManager,this.$viewPort=n.$viewPort},_getDirection:function(n){switch(n){case u.back:return"backward";case u.blank:return"forward";default:return"none"}},_showViewImpl:function(t,i){var u=this,e=t.renderResult.$markup,o,s;return u._equalLayouts(u.$viewPort,e)?(o=n.map(u.$viewPort.find(r),function(t){var r=n(t),o=r.data("placeholder"),s=r.data("transition");return{destination:r,source:e.find(f(o)),type:s||"none",direction:u._getDirection(i)}}),s=u._executeTransitions(o).done(function(){u._changeView(t)}),s):(u._changeView(t),n.Deferred().resolve().promise())},showView:function(n,t){return this._showViewImpl(n,t)},_equalLayouts:function(t,i){var e=t.find(r),o=i.find(r),u;return e.length!==o.length?!1:(u=!0,e.each(function(){var t=n(this).data("placeholder");if(o.filter(f(t)).length!==1){u=!1;return}}),u)},_changeView:function(n){var t=n.renderResult.$markup;this.$viewPort.children().detach(),this.$viewPort.append(t),this._updateNavigationSelectedItem(n),t.show()},_updateNavigationSelectedItem:function(){},_executeTransitions:function(t){var i=this,r=n.grep(t,function(n){return n.type!=="none"}),u=n.map(r,n.proxy(i._executeTransition,i));return n.when.apply(n,u).promise()},_executeTransition:function(n){var i=t.framework.html.TransitionExecutor.create(this.$viewPort,n.type);return i.exec(n)}}),n(function(){t.framework.html.layoutControllers.empty=new t.framework.html.DefaultLayoutController})}(jQuery,DevExpress),function(n,t,i){var r=t.Class;t.framework.html.KnockoutJSTemplateEngine=r.inherit({ctor:function(n){this.navigationManager=n.navigationManager,this._registerUriBinding()},applyTemplate:function(n,t){ko.applyBindings(t,n)},_registerUriBinding:function(){var t=this,r=/{([_A-Za-z0-9.]+)}/gm;ko.bindingHandlers.uri={init:function(u,f,e,o){var c=f(),s=n(u),h;ko.computed(function(){var n=c.replace(r,function(n,t){var e=t.split("."),f=o,s,h;for(s in e)if(h=e[s],f!==i)f=ko.utils.unwrapObservable(f[h]);else break;return f===i?n:f});s.data("uri",n)}),h=function(){return t.navigationManager.navigate(s.data("uri")),!1};s.on("click",h);n(u).data("commandValueAccessor",function(){return{execute:h}})},update:function(){}}}})}(jQuery,DevExpress),function(n,t,i){var r=t.Class;t.framework.html.ViewEngine=r.inherit({ctor:function(t){t=t||{},this.$root=t.$root,this.device=t.device||{},this.templateEngine=t.templateEngine,this.commandManager=t.commandManager,this._defaultLayout=t.defaultLayout||"empty",this._templateMap={},this._pendingViewContainer=null,this.viewSelecting=n.Callbacks(),this.layoutSelecting=n.Callbacks(),this.modelFromViewDataExtended=n.Callbacks(),this.layoutApplying=n.Callbacks(),this.layoutApplied=n.Callbacks()},_findTemplate:function(t,i){var u=this,f=n.grep(this._templateMap[t]||[],function(n){return n.data("role")==i}),r;if(f.length==0)throw new Error("Template not found. role:  "+i+", name: "+t);return r=-1,n.each(f,function(t,i){var f=0;n.each(u.device,function(n){var t=i.data(n);t===u.device[n]&&f++}),f>r&&(r=f,$template=i)}),$template.clone()},_viewSelecting:function(t){var i={viewName:t};return this.viewSelecting.fire(i),i.view?n(i.view):this._findTemplate(t,"view")},_layoutSelecting:function(t){var i={layoutName:t};return this.layoutSelecting.fire(i),i.layout?n(i.layout):this._findTemplate(t,"layout")},_extendModelFromViewData:function(n,t){for(var u=n.get(0).attributes,f,e,r=0;r<u.length;r++)f=u[r].name,f.indexOf("data-")==0&&(e=f.substr(5),t[e]===i&&(t[e]=u[r].value));this.modelFromViewDataExtended.fire({view:n,model:t})},_loadTemplatesFromMarkup:function(t){var i=this;n(["view","layout"]).each(function(r,u){t.filter("[data-role='"+u+"']").each(function(t,r){var u=n(r),f=u.data("name"),e=i._templateMap[f]||[];e.push(u),i._templateMap[f]=e,u.detach()})})},_applyLayout:function(t,i){var r={$view:t,$layout:i},u;return this.layoutApplying.fire(r),u=r.$markup?n(r.$markup):this._applyLayoutCore(t,i),this.layoutApplied.fire({$markup:u}),u},_applyLayoutCore:function(t,i){return n.each(t.children("*[data-target-placeholder]"),function(){var t=n(this),r=t.data("target-placeholder");i.find("*[data-placeholder='"+r+"']").append(t)}),i},_applyPartialViews:function(t){var i=this;n.each(t.find("*[data-render-partial]"),function(){var r=n(this),u=r.data("render-partial"),t=i._findTemplate(u,"view").clone();i._applyPartialViews(t),r.append(t),t.show()})},_createMarkupFromString:function(t){var i=document.createElement("div");return window.WinJS?WinJS.Utilities.setInnerHTMLUnsafe(i,t):i.innerHTML=t,n(i).contents()},_loadTemplates:function(){var t=this,i;return this._templateMap={},this._loadTemplatesFromMarkup(this.$root.children()),i=[],n("head").find("link[rel='dx-template']").each(function(r,u){var f=n(u).attr("href"),e=n.ajax({url:f,success:function(n){t._loadTemplatesFromMarkup(t._createMarkupFromString(n))},dataType:"html"});i.push(e)}),n.when.apply(n,i).done(function(){t._loaded=!0})},_addPendingViewMarkupToDocument:function(t){this._pendingViewContainer=n("<div><\/div>").append(t).appendTo(document.documentElement)},_detachPendingViewMarkupFromDocument:function(){this._pendingViewContainer.detach(),this._pendingViewContainer=null},_renderViewCore:function(n,t){var r=this._viewSelecting(n),f;this._extendModelFromViewData(r,t);var e=r.data("layout")||this._defaultLayout,u=this._layoutSelecting(e),i=this._applyLayout(r,u);return this._applyPartialViews(i),this._addPendingViewMarkupToDocument(i),this.templateEngine.applyTemplate(i.get(0),t),this.commandManager.layoutCommands(i,r,t),f=u.data("controller"),{$markup:i,layoutControllerName:f}},renderView:function(t,i){var r=this,u=n.Deferred(),f;return this._loaded?(f=r._renderViewCore(t,i),r._detachPendingViewMarkupFromDocument(),u.resolveWith(r,[f])):r._loadTemplates().done(function(){var n=r._renderViewCore(t,i);r._detachPendingViewMarkupFromDocument(),u.resolveWith(r,[n])}),u.promise()}})}(jQuery,DevExpress),function(n,t){var r=t.Class;t.framework.html.HtmlApplication=t.framework.Application.inherit({ctor:function(i){this.callBase(i);var r=window.sessionStorage&&sessionStorage.getItem("dx-simulator-device");this.device=i.device||t.framework.html.devices.getDevice(r),this._$root=n(i.rootNode||document.body),this._$viewPort=n(i.viewPortNode||document.body),this.viewEngine=i.viewEngine||new t.framework.html.ViewEngine({$root:this._$root,device:this.device,defaultLayout:i.defaultLayout,templateEngine:i.templateEngine||new t.framework.html.KnockoutJSTemplateEngine({navigationManager:this.navigationManager}),commandManager:i.commandManager||new t.framework.html.CommandManager({globalCommands:i.navigation})}),this.viewRendered=n.Callbacks(),this._initLayoutControllers(),this._applyCssTheme(i)},_applyCssTheme:function(t){var i=this,r=t.themeClasses||this._getThemeClasses(this.device);n(function(){i._$viewPort.addClass(r)})},_getThemeClasses:function(n){var i={ios:"dx-theme-ios dx-theme-ios-typography",android:"dx-theme-android dx-theme-android-typography",desktop:"dx-theme-desktop dx-theme-desktop-typography",win8:"dx-theme-win8 dx-theme-win8-typography",win8phone:"dx-theme-win8phone dx-theme-win8phone-typography"},t=n.platform;return n.platform==="win8"&&(t=n.platform+(n.phone?"phone":"")),i[t]},_initLayoutControllers:function(){var i=this;n.each(t.framework.html.layoutControllers,function(n,t){t.init&&t.init({app:i,$viewPort:i._$viewPort,navigationManager:i.navigationManager})})},_setCurrentViewAsyncImpl:function(n,t){var i=this;return this._ensureRenderedAsync(n).done(function(){i._showRenderedView(n,t)})},_showRenderedView:function(n,i){var r=n.renderResult.layoutControllerName||"empty",u=t.framework.html.layoutControllers[r];t.enqueue(function(){return u.showView(n,i)})},_ensureRenderedAsync:function(t){var r=this,i=n.Deferred();return t.renderResult?i.resolve().promise():(this.viewEngine.renderView(t.viewName,t.model).done(function(n){t.renderResult=n,r.viewRendered.fire(t);var u=t.model.viewRendered;u&&u.call(t.model,t),i.resolve()}),i.promise())}})}(jQuery,DevExpress),function(n,t){var s=t.Class,r=400,u=t.Class.inherit({ctor:function(n){this.container=n},exec:function(t){var i=this,f=t.source,r=t.destination,h=i._getElementProps(r),e=i._createWrapperProps(r),u=i._wrapElementContent(f,e),o=i._wrapElementContent(r,e),s=i._getElementDomLocation(u);return u.insertAfter(o),i._animate(n.extend({},t,{source:u,destination:o})).done(function(){i._restoreElementDomLocation(u,s),i._unwrapElement(r),i._unwrapElement(f)})},_getElementProps:function(t){var i={};return i.style=t.attr("style")||"",n.each(["position","top","left"],function(n,r){i[r]=t.css(r)}),i},_createWrapperProps:function(n){return{position:"absolute",top:0,left:n.position().left,width:n.outerWidth(!0),height:n.outerHeight(!0)}},_wrapElementContent:function(t,i){var r=n("<div />").css(i).css({position:"relative"}),u;return t.wrapInner(r),r=t.children().eq(0),u=n("<div />").css(i),r.wrapInner(u),r.children().eq(0)},_unwrapElement:function(n){var t=n.children().eq(0),i=t.children().eq(0);i.children().eq(0).unwrap().unwrap()},_getElementDomLocation:function(n){return{$parent:n.parent()}},_restoreElementDomLocation:function(n,t){var i=t.$parent;i.append(n)},_animate:function(){return(new n.Deferred).resolve().promise()}}),f=u.inherit({_setElementLeft:function(n,i){!1&&t.support.transform3d?n.css(t.support.styleProp("transform"),"translate3d("+i+"px, 0px, 0px)"):n.css("left",i)},_animate:function(t){var e=this,u=t.source,f=t.destination,h=f.position().top,o=f.position().left,i=this.container.width(),s=new n.Deferred;return t.direction==="backward"&&(i=-i),u.prop("dxTransitionLeft",i+o),u.animate({dxTransitionLeft:o},{duration:r,step:function(n){e._setElementLeft(f,n-i),e._setElementLeft(u,n)},complete:function(){s.resolve()}}),s.promise()}}),e=u.inherit({_animate:function(t){var e=t.source,i=t.destination,s=i.position().top,u=i.position().left,f=this.container.width(),o=new n.Deferred;return t.direction==="backward"&&(f=-f),e.css({top:s,left:f+u}),t.direction==="forward"?e.animate({left:u},r,function(){o.resolve()}):(e.css({left:u,"z-index":1}),i.css({"z-index":2}),i.animate({left:-f+u},r,function(){o.resolve()})),o.promise()}}),o=u.inherit({_animate:function(t){var i=t.source,f=t.destination,u=new n.Deferred;return i.css({opacity:0}),f.animate({opacity:0},r),i.animate({opacity:1},r,function(){u.resolve()}),u.promise()}});u.create=function(n,t){switch(t){case"slide":return new f(n);case"fade":return new o(n);case"overflow":return new e(n);default:throw Error('Unknown transition type "'+t+'"');}},t.framework.html.TransitionExecutor=u}(jQuery,DevExpress),function(n,t){var r=t.framework.html,u,f={iPhone:"iPhone",iPad:"iPad",androidPhone:"Android Mobile",androidTablet:"Android",win8:"MSAppHost",win8Phone:"Windows Phone",desktop:"desktop"},e={phone:!1,tablet:!1,android:!1,ios:!1,win8:!1,platform:"desktop"},o=function(n){var o=/ipad/i.test(n),r=/iphone|ipod/i.test(n),t=/android/i.test(n),u=/windows phone/i.test(n),f=/msapphost/i.test(n)||u,i,s;return!o&&!r&&!t&&!f&&!u?e:(i=r||t&&/mobile/i.test(n)||u,s=!i&&!f,name=t?"android":f?"win8":"ios",{phone:i,tablet:!i,android:t,ios:o||r,platform:name})},s=function(n){var t;if(n){if(t=f[n],!t)throw Error("Unknown device");}else t=navigator.userAgent;return o(t)};u=r.devices={getDevice:s}}(jQuery,DevExpress)