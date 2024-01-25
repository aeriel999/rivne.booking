import classNames from "classnames";
import { FC, InputHTMLAttributes, ReactNode } from 'react';


interface InputGroupProps extends InputHTMLAttributes<HTMLInputElement> {
  label: string,
  type?: "text"|"password"|"email"|"number",   //може не передаватися у пропсах для компонента(| - один із можливих варіатнів, які можуть буть)
  field: string,
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void,
  errors?: string[],
  error?: string|undefined,
  touched?: boolean|undefined,
  icon?: ReactNode
}

const InputGroup : FC<InputGroupProps> = ({
                                            label,
                                            type="text", //Якщо не передає значення у type - то буде "text"
                                            field,
                                            value,
                                            onChange,
                                            errors,
                                            error,
                                            touched,
                                            icon
                                          }) => {
  return (
    <div className="mb-3">
      <label
        className="mb-3 block text-sm font-medium text-black dark:text-white"
        htmlFor={label}
      >
        {label}
      </label>
      <div className="relative">
                        <span className="absolute left-4.5 top-4">
                         {icon}
                        </span>
      <input
        type={type}
        className={classNames("w-full rounded border border-stroke bg-gray py-3 pl-11.5 pr-4.5 text-black " +
          "focus:border-primary focus-visible:outline-none dark:border-strokedark dark:bg-meta-4 dark:text-white dark:focus:border-primary", {
          "is-invalid": errors || (error && touched)
        })}
        id={field}
        name={field}
        value={value}
        onChange={onChange}
        aria-describedby="emailHelp"
      />
      {errors && (
        <div id="validationServerUsernameFeedback" className="invalid-feedback">
          {errors.map((err, index) => (
            <span key={index}>{err}</span>
          ))}
        </div>
      )}
      {(error && touched) && (
        <div id="validationServerUsernameFeedback" className="invalid-feedback">
          <span style={{ color: 'red' }}>{error}</span>
        </div>
      )}
      </div>
    </div>
  );
};

export default InputGroup;