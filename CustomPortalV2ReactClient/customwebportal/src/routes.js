import React from 'react' 


const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'))
const BranchDefination = React.lazy(() => import('./views/branchdefination/'))
const AdressDefination = React.lazy(() => import('./views/adressdefination'))
const FormDefinationType = React.lazy(() => import('./views/formdefinations'))
const ProductDefination = React.lazy(() => import('./views/productdefination')) 
const UserDefination = React.lazy(() => import('./views/userdefination'))
const FormDefinationEdit=  React.lazy(() => import('./views/formdefinationEdit'))
const DigitalForms=React.lazy(() => import('./views/digitalForms'))
const DigitalFormEdit=React.lazy(() => import('./views/digitalFormEdit'))
const FormDefinationAutoComplate=React.lazy(() => import('./views/formdefinationsAComp'))
const FormDefinationVersion=React.lazy(()=>import('./views/formdefinationversion') );
const FormDefinationAttachments=React.lazy(()=>import('./views/definationattachments') );
const WorkFlowDefination = React.lazy(()=>import('./views/workflowDefination'));
const workflow = React.lazy(()=>import('./views/workflow'));
const Settings = React.lazy(()=>import('./views/settings'));
const UserProfile = React.lazy(()=>import('./views/Profile'));
const CustomeFields=React.lazy(()=>import('./views/customefields'));
const customeFieldEdit=React.lazy(()=>import('./views/customefieldEdit'));


const routes = [

  
  { path: '/', exact: true, name: 'Home' },
  { path: '/dashboard', name: 'Dashboard', element: Dashboard }, 
  { path: '/BranchDefination', name: 'BranchDefination', element: BranchDefination },   
  { path: '/AdressDefination', name: 'AdressDefination', element: AdressDefination }, 
  { path: '/FormDefinationType', name: 'FormDefinationType', element: FormDefinationType }, 
  { path: '/productdefination', name: 'productdefination', element: ProductDefination }, 
  { path: '/userdefination', name: 'Userdefination', element: UserDefination }, 
  { path: '/FormDefinationTypeEdit', name: 'FormDefinationType/FormDefinationEdit', element: FormDefinationEdit }, 
  
  { path: '/digitalForms', name: 'DigitalForms', element: DigitalForms }, 
  { path: '/digitalFormEdit', name: 'DigitalForms/digitalFormEdit', element: DigitalFormEdit }, 
  { path: '/formdefinationsAComp', name: 'FormDefinationType/FormDefinationEdit/AutoComplate', element: FormDefinationAutoComplate }, 
  { path: '/FormDefinationVersion', name: 'FormDefinationType/FormDefinationVersion', element: FormDefinationVersion }, 
  { path: '/definationattachments', name: 'FormDefinationType/FormDefinationEdit/definationattachments', element: FormDefinationAttachments }, 

  { path: '/workflowDefination', name: 'workflowDefination', element: WorkFlowDefination }, 
  { path: '/workflow', name: 'workflow', element: workflow }, 
  { path: '/Settings', name: 'Settings', element: Settings }, 
  { path: '/UserProfile', name: 'UserProfile', element: UserProfile }, 
  { path: '/customeFields', name: 'CustomeFields', element: CustomeFields }, 
  { path: '/CustomeFieldEdit', name: 'CustomeFieldEdit', element: customeFieldEdit }, 

  
  
  
  
  
]
export default routes
