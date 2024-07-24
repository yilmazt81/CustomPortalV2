 
   
import  i18   from  '../../translation/i18'
 
 
  
export  const Gridcolumns = [
  {
    accessorKey: 'id', //access nested data with dot notation
    header: 'Id',
    size: 50,
  },
  {
    accessorKey: 'formName',
    header:  i18.t('FormName'),
    size: 150,
  }, 
  {
    accessorKey: 'createdBy',
    header:  i18.t('CreatedBy'),
    size: 150,
  },
  {
    accessorKey: 'createdDate',
    header:  i18.t('CreatedDate'),
    type: 'date',
    dateSetting: {
      format: 'dd/MM/yyyy'
    },
    size: 150,
  },
  {
    accessorKey: 'customSectorName',
    header:  i18.t('CustomSectorName'),
    size: 150,
  }, 
]; 

export default {
  Gridcolumns,
}