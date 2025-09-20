import { InputAdornment, TextField } from '@mui/material';
import Search from 'components/icons/common/Search';

const SearchBox = () => {
  return (
    <TextField
      id="input-with-searchIcon-textfield"
      InputProps={{
        startAdornment: (
          <InputAdornment
            position="start"
            sx={{
              borderRight: 2,
              borderRightColor: 'warning.light',
              height: 20,
              pr: 1,
              mr: 1,
              color: 'neutral.main',
            }}
          >
            <Search fontSize="small" />
          </InputAdornment>
        ),
      }}
      type="text"
      variant="filled"
      placeholder="Search or type"
      fullWidth
      hiddenLabel
      sx={{
        display: { xs: 'none', md: 'block' },
        maxWidth: 348,
      }}
    />
  );
};

export default SearchBox;
