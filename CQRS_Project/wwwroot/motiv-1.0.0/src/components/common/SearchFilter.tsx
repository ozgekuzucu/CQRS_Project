import { TextField } from '@mui/material';
import { GridApiCommunity } from '@mui/x-data-grid/internals';
import Search from 'components/icons/common/Search';
import { ChangeEvent, MutableRefObject } from 'react';

const SearchFilter = ({ apiRef }: { apiRef: MutableRefObject<GridApiCommunity> }) => {
  const handleFilter = (event: ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;
    const searchText = value.toLowerCase();
    apiRef.current.setQuickFilterValues([searchText]);
  };

  return (
    <TextField
      InputProps={{
        startAdornment: <Search fontSize="small" sx={{ mr: 1, color: 'neutral.main' }} />,
      }}
      type="search"
      variant="filled"
      placeholder="Search..."
      onChange={handleFilter}
      size="small"
    />
  );
};

export default SearchFilter;
