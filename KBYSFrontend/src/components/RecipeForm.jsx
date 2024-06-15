import { useFormik } from 'formik';
import PropTypes from 'prop-types';
import { Button, Form } from 'react-bootstrap';

const RecipeForm = ({ onSubmit }) => {
  const formik = useFormik({
    initialValues: {
      ingredients: '',
      mealType: '',
      cookingTime: '',
    },
    onSubmit: (values) => {
      onSubmit(values);
    },
  });

  return (
    <Form onSubmit={formik.handleSubmit}>
      <Form.Group className="mb-3" controlId="formIngredients">
        <Form.Label>Malzemeler</Form.Label>
        <Form.Control
          type="text"
          name="ingredients"
          value={formik.values.ingredients}
          onChange={formik.handleChange}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="formMealType">
        <Form.Label>Öğün Türü</Form.Label>
        <Form.Control
          type="text"
          name="mealType"
          value={formik.values.mealType}
          onChange={formik.handleChange}
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="formCookingTime">
        <Form.Label>Yapılış Süresi</Form.Label>
        <Form.Control
          type="text"
          name="cookingTime"
          value={formik.values.cookingTime}
          onChange={formik.handleChange}
        />
      </Form.Group>

      <div className="text-center">
        <Button variant="primary" type="submit">
          Tarifi Getir
        </Button>
      </div>
    </Form>
  );
};

RecipeForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
};

export default RecipeForm;
