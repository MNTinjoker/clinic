import { Component, ViewChild } from '@angular/core';
import { PatientService } from '../../_services/patient.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import moment from 'moment-jalaali';
import { TreatmentsService } from '../../_services/treatments.service';
import { SharedModule } from '../../share/shared.module';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { QuestionService } from '../../_services/question.service';
import { PdfMakerComponent } from '../../share/pdf-maker/pdf-maker.component';
import { UtilService } from '../../_services/util.service';
export const ValidFormat = ['pdf', 'jpg', 'jpeg', 'png'];

@Component({
  selector: 'app-patient-treatment',
  standalone: true,
  imports: [SharedModule, CommonModule, PdfMakerComponent],
  templateUrl: './patient-treatment.component.html',
  styleUrl: './patient-treatment.component.css'
})
export class PatientTreatmentComponent {
  patientName: string;
  selectedId: string;
  patientInfo: any = [];
  patientServiceList: any = [];
  questionsPerSectionList: any = [];
  patientServiceListTab: any = [];
  selectedService: any = null;
  public Editor = ClassicEditor;
  @ViewChild(PdfMakerComponent) pdfMakerComponent!: PdfMakerComponent;
  showAttacheFile: boolean = false;
  fileToUpload: any;
  base64: any;
  fileName: any;
  fileType: any;

  constructor(
    private patientService: PatientService,
    private toastR: ToastrService,
    private activeRoute: ActivatedRoute,
    private treatmentService: TreatmentsService,
    private questionService: QuestionService,
    private utilService: UtilService

  ) { }
  titleList: any[] = [
    { name: "جناب", code: "1" },
    { name: "دکتر", code: "2" },
    { name: "آقا", code: "3" },
    { name: "خانم", code: "4" },
    { name: "پروفسور", code: "5" },
    { name: "مهندس", code: "6" },
  ];
  async ngOnInit() {
    this.selectedId = this.activeRoute.snapshot.paramMap.get('id');
    await this.getPatientById();
    await this.getPatientServices();
    await this.getPatientTreatments();
  }

  async getPatientById() {
    let res: any = await this.patientService.getPatientById(this.selectedId).toPromise();
    if (res.length > 0) {
      this.patientInfo = res[0];
      this.patientName = res[0].firstName + " " + res[0].lastName;
      const today = moment();
      const birth = moment(this.patientInfo.birthDate, 'jYYYY/jMM/jDD');
      this.patientInfo.age = today.diff(birth, 'years');
      if (this.patientInfo.gender) {
        // this.patientInfo.title = this.titleList.filter(x => x.code == this.patientInfo.titleId)['name'];
        this.patientInfo.title = this.patientInfo.gender == 1 ? 'آقا' : 'خانوم';
      }
    }
  }

  async getPatientServices() {
    let res: any = await this.treatmentService.getPatientServices(this.selectedId).toPromise();
    this.patientServiceListTab = res;
    const grouped = Object.values(
      res.reduce((acc, item) => {
        const key = item.itemCategoryId;
        if (!acc[key]) {
          acc[key] = {
            id: item.itemCategoryId,
            name: item.itemCategoryName,
            values: []
          }
        }
        acc[key].values.push(item);
        return acc;
      }, {} as Record<number, { id: number; name: string; values: typeof res }>)
    )
    this.patientServiceList = grouped;
  }

  async getSectionPerService(id) {
    this.questionsPerSectionList = [];
    let res: any = await this.treatmentService.getSectionPerService(id).toPromise();
    await res.forEach(item => {
      this.treatmentService.getQuestionsPerSection(item.id).subscribe((res: any) => {
        res.forEach(question => {
          if (question.type == "MultiSelect" || question.type == "Check" || question.type == "Combo" || question.type == "Radio" || question.type == "textCombo") {
            this.treatmentService.getAnswersPerQuestion(question.id).subscribe((data: any) => {
              question.answers = data.map(item => ({
                id: item.id,
                title: item.title,
                text: item.text
              }));
              ;
            });
          }
        });
        this.questionsPerSectionList.push({
          id: item.id,
          name: item.title,
          invoiceItemId: this.selectedService.invoiceItemId,
          values: res,
          isOpen: true
        });
      });
    });
    setTimeout(() => {
      this.setValues();
    }, 1000);
  }

  onClick(event: MouseEvent, service, type) {
    event.stopPropagation();
    switch (type) {
      case 1:
        this.selectedService = service;
        this.getSectionPerService(service.treatmentTemplateId);
        break;
      case 4:
        this.fileToUpload = null;
        this.base64 = null;
        this.fileName = null;
        this.fileType = null;
        this.showAttacheFile = true;
        break;
    }
  }

  onClickTab(event: MouseEvent, id, type) {
    event.stopPropagation();
    switch (type) {
      case 1:
        this.getAllValues();
        this.selectedService = null;
        break;
      case 2:
        break;
      case 4:
        this.selectedService = null;
        break;
    }
  }

  async getAllValues() {
    const result = [];
    await this.questionsPerSectionList.forEach(section => {
      const sectionData = {
        sectionId: section.id,
        sectionName: section.name,
        values: []
      }

      section.values.forEach(item => {
        let value;
        let answerId;
        switch (item.type) {
          case 'Text':
          case 'Paragraph':
          case 'Label':
          case 'Editor':
            value = item.value;
            answerId = null;
            break;
          case 'Combo':
          case 'textCombo':
          case 'Radio':
            answerId = item.value?.toString();
            value = null;
            break;
          // case 'MultiSelect':
          //   answerId = (item.value || []).map(opt => opt.id).join(',') || "";
          //   value = null;
          //   break;
          case 'Check':
          case 'MultiSelect':
            answerId = (item.value || []).join(',') || "";
            value = null;
            break;
        }
        let temp = {
          id: item.id,
          title: item.title,
          selectedValue: value,
          answerId: answerId
        }
        if (value || answerId) {
          this.saveQuestionValue(temp);
        }
        sectionData.values.push(temp);
      });
      result.push(sectionData);
    });
    this.getPatientTreatments();
  }

  onCheckboxChange(event: any, item: any) {
    if (!item.value) {
      item.value = [];
    }
    const optionId = event.target.value;
    if (event.target.checked) {
      item.value.push(optionId);
    } else {
      item.value = item.value.filter(id => id !== optionId);
    }
  }

  async saveQuestionValue(item) {
    let model = {
      questionId: item.id,
      selectedValue: item.selectedValue,
      invoiceItemId: this.selectedService.invoiceItemId,
      answerId: item.answerId
    }
    try {
      await this.questionService.saveQuestionValue(model).toPromise();
    } catch (error) {
      this.toastR.error('خطا!', 'خطا در ثبت ')
    }
  }

  async getPatientTreatments() {
    let res: any = await this.treatmentService.getPatientTreatments(this.selectedId).toPromise();
    if (res.length > 0) {
      res.forEach(element => {
        let index = this.patientServiceListTab.findIndex(item => item.invoiceItemId == element.invoiceItemId);
        this.patientServiceListTab[index]['sections'] = element.sections;
        this.patientServiceListTab[index]['sections'].forEach(element => {
          element.isOpen = true;
          element.questions.forEach(question => {
            switch (question.type) {
              case 'Combo':
              case 'textCombo':
              case 'Radio':
              case 'MultiSelect':
              case 'Check':
                question['selectedValue'] = (question.answers || []).map(opt => opt.title).join(',');
                break;
              case 'Editor':
                question['selectedValue'] = question['selectedValue'] ?? '';
                break;
            }
          });
        });
      });
    }
  }

  async setValues() {
    await this.questionsPerSectionList.forEach(element => {
      let index = this.patientServiceListTab.findIndex(item => item.invoiceItemId == element.invoiceItemId);
      let index2 = this.patientServiceListTab[index]['sections'].findIndex(item => item.id == element.id);
      this.patientServiceListTab[index]['sections'][index2]['questions'].forEach(questions => {
        let index3 = element.values.findIndex(item => item.id == questions.id);
        let value = null;
        switch (questions.type) {
          case 'Text':
          case 'Paragraph':
          case 'Label':
          case 'Editor':
            value = questions['selectedValue'] ?? '';
            break;
          case 'Combo':
          case 'textCombo':
            value = (questions.answers || []).map(opt => opt.id).join(',') || "";
            break;
          case 'Radio':
            value = +((questions.answers || []).map(opt => opt.id));
            break;
          case 'MultiSelect':
            value = (questions.answers || []).map(opt => opt.id);
            break;
          case 'Check':
            value = (questions.answers || []).map(opt => opt.id);
            break;
        }
        element.values[index3]['value'] = value;
      });
    });
  }

  handelCallMetodInPdfMaker(event: MouseEvent, item, type) {
    event.stopPropagation();
    this.pdfMakerComponent.selectedServiceForPDF2 = item;
    if (type == 1) {
      this.pdfMakerComponent.generatePDF('print');
    } else {
      this.pdfMakerComponent.generatePDF('download');
    }
  }

  handleFileInput(files: any) {
    let size = files[0].size;
    let type = files[0]['name'].split('.').pop();
    if (!ValidFormat.includes(type.toLowerCase())) {
      this.toastR.error("فرمت وارد شده معتبر نمی باشد.", "خطا");
      return;
    }
    if (size > 50000000) {
      this.toastR.error("حداکثر سایز فایل 50 مگابایت می باشد", "خطا");
      return;
    }
    this.fileToUpload = files.item(0);
    this.utilService.getBase64(files.item(0)).then((data) => {
      let base: any = data;
      this.base64 = base.split(',')[1];

      this.fileName = this.fileToUpload['name'];
      this.fileType = this.fileToUpload['name'].split('.').pop();
    });
  }

  removeFile(event) {
    event.stopPropagation();
    this.fileName = '';
    this.fileType = '';
    this.fileToUpload = null;
    this.base64 = null;
  }
}