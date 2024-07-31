
import  i18   from  '../../translation/i18'
 
 
  
export  const GridcolumnsGroups = [
  {
    accessorKey: 'id', //access nested data with dot notation
    header: 'Id',
    size: 50,
  },
  {
    accessorKey: 'name',
    header:  i18.t('Name'),
    size: 150,
  }, 
  {
    accessorKey: 'groupTag',
    header:  i18.t('GroupTag'),
    size: 150,
  },
   
]; 

export default {
    GridcolumnsGroups,
}