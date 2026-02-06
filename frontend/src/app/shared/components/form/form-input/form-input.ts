import {Component, computed, input, model} from '@angular/core';
import {FormValueControl, ValidationError, WithOptionalField} from '@angular/forms/signals';
import {InputText} from 'primeng/inputtext';

@Component({
  selector: 'app-form-input',
  imports: [
    InputText
  ],
  templateUrl: './form-input.html',
  styleUrl: './form-input.scss',
  standalone: true,
})
export class FormInput implements FormValueControl<string>{
  id = input.required<string>();
  label = input.required<string>();
  autocomplete = input<string>('off');
  placeholder = input<string>('');
  type = input<string>('text');
  invalid = input(false);
  dirty = input(false);
  touched = input(false);
  errors = input<readonly WithOptionalField<ValidationError>[]>([]);

  value = model<string>('');

  showErrors = computed(() => (this.touched() || this.dirty()) && this.invalid())

  onInput(event: Event) {
    this.value.set((event.target as HTMLInputElement).value ?? '');
  }
}
