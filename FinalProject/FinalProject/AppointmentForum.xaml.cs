﻿using FinalProject.Tables;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentForum : ContentPage
    {
        Doctor doctor;
        User user;
        bool t0, t1, t2, t3, t4, t5, t6, p0renew, p1renew, p2renew = false;
        public AppointmentForum(Appointment appointment)
        {
            InitializeComponent();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                user = conn.GetWithChildren<User>(appointment.uId);
                doctor = conn.GetWithChildren<Doctor>(appointment.dId);
                //var doctors = conn.Query<Doctor>("select * from Doctor where Id=?", appointment.dId);
                //doctor = doctors[0];
            }
            Rx0EndDate.IsVisible = false;
            Rx0EndLabel.IsVisible = false;
            Rx0Label.IsVisible = false;
            Rx0Name.IsVisible = false;
            Rx0StartDate.IsVisible = false;
            Rx0StartLabel.IsVisible = false;
            Rx0More.IsVisible = false;
            togSwitch1.IsVisible = false;
            Rx1EndDate.IsVisible = false;
            Rx1EndLabel.IsVisible = false;
            Rx1Label.IsVisible = false;
            Rx1Name.IsVisible = false;
            Rx1StartDate.IsVisible = false;
            Rx1StartLabel.IsVisible = false;
            Rx1More.IsVisible = false;
            togSwitch2.IsVisible = false;
            Rx2EndDate.IsVisible = false;
            Rx2EndLabel.IsVisible = false;
            Rx2Label.IsVisible = false;
            Rx2Name.IsVisible = false;
            Rx2StartDate.IsVisible = false;
            Rx2StartLabel.IsVisible = false;
            Vaccine0Label.IsVisible = false;
            Vaccine0Name.IsVisible = false;
            Vaccine1Label.IsVisible = false;
            Vaccine1More.IsVisible = false;
            Vaccine1Name.IsVisible = false;
            togSwitch4.IsVisible = false;
            Vaccine2Label.IsVisible = false;
            Vaccine2More.IsVisible = false;
            Vaccine2Name.IsVisible = false;
            togSwitch5.IsVisible = false;
            FollowUpDateEntry.IsVisible = false;
            FollowUpTimeEntry.IsVisible = false;
            FollowUpDateLabel.IsVisible = false;
            FollowUpTimeLabel.IsVisible = false;
        }

        async void ButtonClicked(object sender, EventArgs e)
        {
            //Creates the appointment item to be inserted in the database
            Appointment appointment = new Appointment()
            {
                aptDate = AppointmentDateEntry.Date.Add(AppointmentTimeEntry.Time),
                reasonForVisit = Reason.Text,
                diagnosis = Diagnosis.Text,
                followUpAdvice = FollowUpRecsEntry.Text,
                dId = doctor.Id,
                uId = user.Id
            };
            //Creates the prescription 0 item to be inserted in the database
            Prescription p0 = new Prescription()
            {
                RxName = Rx0Name.Text,
                startDate = Rx0StartDate.Date.ToShortDateString(),
                endDate = Rx0EndDate.Date.ToShortDateString(),
                dId = doctor.Id,
                uId = user.Id
            };
            //Creates the prescription 1 item to be inserted in the database
            Prescription p1 = new Prescription()
            {
                RxName = Rx1Name.Text,
                startDate = Rx1StartDate.Date.ToShortDateString(),
                endDate = Rx1EndDate.Date.ToShortDateString(),
                dId = doctor.Id,
                uId = user.Id
            };
            //Creates the prescription 2 item to be inserted in the database
            Prescription p2 = new Prescription()
            {
                RxName = Rx2Name.Text,
                startDate = Rx2StartDate.Date.ToShortDateString(),
                endDate = Rx2EndDate.Date.ToShortDateString(),
                dId = doctor.Id,
                uId = user.Id
            };

            //Creates the vaccine 0 item to be inserted in the database
            Vaccine v0 = new Vaccine()
            {
                VaccineName = Vaccine0Name.Text,
                Date = AppointmentDateEntry.Date.ToShortDateString(),
                dId = doctor.Id,
                uId = user.Id
            };
            //Creates the vaccine 1 item to be inserted in the database
            Vaccine v1 = new Vaccine()
            {
                VaccineName = Vaccine1Name.Text,
                Date = AppointmentDateEntry.Date.ToShortDateString(),
                dId = doctor.Id,
                uId = user.Id
            };
            //Creates the vaccine 2 item to be inserted in the database
            Vaccine v2 = new Vaccine()
            {
                VaccineName = Vaccine2Name.Text,
                Date = AppointmentDateEntry.Date.ToShortDateString(),
                dId = doctor.Id,
                uId = user.Id
            };
            //Creates a future appointment for the user
            Appointment fa = new Appointment()
            {
                aptDate = FollowUpDateEntry.Date.Add(AppointmentTimeEntry.Time),
                dId = doctor.Id,
                uId = user.Id
            };

            //Adds the appointment to the user's appointment list
            if (user.Appointments == null)
            {
                user.Appointments = new List<Appointment>
                {
                    appointment
                };
            }
            else
            {
                user.Appointments.Add(appointment);
            }

            //Adds the appointment to the doctor's appointment list
            if (doctor.Appointments == null)
            {
                doctor.Appointments = new List<Appointment>
                {
                    appointment
                };
            }
            else
            {
                doctor.Appointments.Add(appointment);
            }
            
            //Checks if the first prescription has a null value
            if (!(p0.RxName.Equals("")))
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    //Ignored if the table already exists
                    conn.CreateTable<Prescription>();
                    conn.Insert(p0);
                }
                //If the prescription has a name, check to see if the user already has a log of said prescription
                //If they do, then just change the end date
                if (user.Prescriptions != null)
                {
                    foreach(Prescription rx in user.Prescriptions)
                    {
                        if (rx.RxName.Equals(p0.RxName))
                        {
                            p0renew = true;
                            rx.endDate = p0.endDate;
                        }
                    }
                    if (p0renew == false)
                    {
                        user.Prescriptions.Add(p0);
                    }
                }
                //Otherwise, add the prescription to the user's list
                else
                {
                    user.Prescriptions = new List<Prescription>
                    {
                        p0
                    };
                }
                //Add the prescription to the doctor's list
                if (doctor.Prescriptions == null)
                {
                    doctor.Prescriptions = new List<Prescription>
                    {
                        p0
                    };
                }
                else
                {
                    doctor.Prescriptions.Add(p0);
                }
            }
            if (!(p1.RxName.Equals("")))
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    //Ignored if the table already exists
                    conn.CreateTable<Prescription>();
                    conn.Insert(p1);
                }
                //If the prescription has a name, check to see if the user already has a log of said prescription
                //If they do, then just change the end date
                if (user.Prescriptions != null)
                {
                    foreach (Prescription rx in user.Prescriptions)
                    {
                        if (rx.RxName.Equals(p1.RxName))
                        {
                            p1renew = true;
                            rx.endDate = p1.endDate;
                        }
                    }
                    if (p1renew == false)
                    {
                        user.Prescriptions.Add(p1);
                    }
                }
                //Otherwise, add the prescription to the user's list
                else
                {
                    user.Prescriptions = new List<Prescription>
                    {
                        p1
                    };
                }
                //Add the prescription to the doctor's list
                if (doctor.Prescriptions == null)
                {
                    doctor.Prescriptions = new List<Prescription>
                    {
                        p1
                    };
                }
                else
                {
                    doctor.Prescriptions.Add(p1);
                }
            }
            if (!(p2.RxName.Equals("")))
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    //Ignored if the table already exists
                    conn.CreateTable<Prescription>();
                    conn.Insert(p2);
                }
                //If the prescription has a name, check to see if the user already has a log of said prescription
                //If they do, then just change the end date
                if (user.Prescriptions != null)
                {
                    foreach (Prescription rx in user.Prescriptions)
                    {
                        if (rx.RxName.Equals(p2.RxName))
                        {
                            p2renew = true;
                            rx.endDate = p2.endDate;
                        }
                    }
                    if (p2renew == false)
                    {
                        user.Prescriptions.Add(p2);
                    }
                }
                //Otherwise, add the prescription to the user's list
                else
                {
                    user.Prescriptions = new List<Prescription>
                    {
                        p2
                    };
                }
                //Add the prescription to the doctor's list
                if (doctor.Prescriptions == null)
                {
                    doctor.Prescriptions = new List<Prescription>
                    {
                        p2
                    };
                }
                else
                {
                    doctor.Prescriptions.Add(p2);
                }
            }
            if (!(v0.VaccineName.Equals("")))
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    //Ignored if the table already exists
                    conn.CreateTable<Vaccine>();
                    conn.Insert(v0);
                }

                if (user.Vaccines != null)
                {
                    user.Vaccines.Add(v0);
                }
                else
                {
                    user.Vaccines = new List<Vaccine>
                    {
                        v0
                    };
                }
            }
            if (!(v1.VaccineName.Equals("")))
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    //Ignored if the table already exists
                    conn.CreateTable<Vaccine>();
                    conn.Insert(v1);
                }

                if (user.Vaccines != null)
                {
                    user.Vaccines.Add(v1);
                }
                else
                {
                    user.Vaccines = new List<Vaccine>
                    {
                        v1
                    };
                }
            }
            if (!(v2.VaccineName.Equals("")))
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    //Ignored if the table already exists
                    conn.CreateTable<Vaccine>();
                    conn.Insert(v2);
                }

                if (user.Vaccines != null)
                {
                    user.Vaccines.Add(v2);
                }
                else
                {
                    user.Vaccines = new List<Vaccine>
                    {
                        v2
                    };
                }
            }
            if (t6)
            {
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<Appointment>();
                    conn.Insert(fa);
                }

                if (user.Appointments != null)
                {
                    user.Appointments.Add(fa);
                }
                else
                {
                    user.Appointments = new List<Appointment>
                    {
                        fa
                    };
                }
            }
            //Adds the appointment to the database
            //updates the user with its new appointment info
            //Updates the doctor with its new appointment info
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                //Ignored if the table already exists
                conn.CreateTable<Appointment>();
                conn.CreateTable<Doctor>();
                conn.CreateTable<User>();
                conn.Insert(appointment);
                conn.Update(user);
                conn.Update(doctor);
            }
            await Navigation.PopModalAsync();
        }
        private void OnToggle0(object sender, ToggledEventArgs e)
        {
            t0 = !t0;
            if (t0)
            {
                Rx0EndDate.IsVisible = true;
                Rx0EndLabel.IsVisible = true;
                Rx0Label.IsVisible = true;
                Rx0Name.IsVisible = true;
                Rx0StartDate.IsVisible = true; 
                Rx0StartLabel.IsVisible = true;
                Rx0More.IsVisible = true;
                togSwitch1.IsVisible = true;
            }
            else
            {
                Rx0EndDate.IsVisible = false;
                Rx0EndLabel.IsVisible = false;
                Rx0Label.IsVisible = false;
                Rx0Name.IsVisible = false;
                Rx0StartDate.IsVisible = false;
                Rx0StartLabel.IsVisible = false;
                Rx0More.IsVisible = false;
                togSwitch1.IsVisible = false;
                Rx1EndDate.IsVisible = false;
                Rx1EndLabel.IsVisible = false;
                Rx1Label.IsVisible = false;
                Rx1Name.IsVisible = false;
                Rx1StartDate.IsVisible = false;
                Rx1StartLabel.IsVisible = false;
                Rx1More.IsVisible = false;
                togSwitch2.IsVisible = false;
                Rx2EndDate.IsVisible = false;
                Rx2EndLabel.IsVisible = false;
                Rx2Label.IsVisible = false;
                Rx2Name.IsVisible = false;
                Rx2StartDate.IsVisible = false;
                Rx2StartLabel.IsVisible = false;
                t1 = false;
                t2 = false;
            }
        }
        private void OnToggle1(object sender, ToggledEventArgs e)
        {
            t1 = !t1;
            if (t1)
            {
                Rx1EndDate.IsVisible = true;
                Rx1EndLabel.IsVisible = true;
                Rx1Label.IsVisible = true;
                Rx1Name.IsVisible = true;
                Rx1StartDate.IsVisible = true;
                Rx1StartLabel.IsVisible = true;
                Rx1More.IsVisible = true;
                togSwitch2.IsVisible = true;
            }
            else
            {
                Rx1EndDate.IsVisible = false;
                Rx1EndLabel.IsVisible = false;
                Rx1Label.IsVisible = false;
                Rx1Name.IsVisible = false;
                Rx1StartDate.IsVisible = false;
                Rx1StartLabel.IsVisible = false;
                Rx1More.IsVisible = false;
                togSwitch2.IsVisible = false;
                Rx2EndDate.IsVisible = false;
                Rx2EndLabel.IsVisible = false;
                Rx2Label.IsVisible = false;
                Rx2Name.IsVisible = false;
                Rx2StartDate.IsVisible = false;
                Rx2StartLabel.IsVisible = false;
                t2 = false;
            }
        }
        private void OnToggle2(object sender, ToggledEventArgs e)
        {
            t2 = !t2;
            if (t2)
            {
                Rx2EndDate.IsVisible = true;
                Rx2EndLabel.IsVisible = true;
                Rx2Label.IsVisible = true;
                Rx2Name.IsVisible = true;
                Rx2StartDate.IsVisible = true;
                Rx2StartLabel.IsVisible = true;
            }
            else
            {
                Rx2EndDate.IsVisible = false;
                Rx2EndLabel.IsVisible = false;
                Rx2Label.IsVisible = false;
                Rx2Name.IsVisible = false;
                Rx2StartDate.IsVisible = false;
                Rx2StartLabel.IsVisible = false;
            }
        }





        private void OnToggle3(object sender, EventArgs e)
        {
            t3 = !t3;

            if(t3)
            {
                Vaccine0Label.IsVisible = true;
                Vaccine1More.IsVisible = true;
                Vaccine0Name.IsVisible = true;
                togSwitch4.IsVisible = true;
            }
            else
            {
                Vaccine0Label.IsVisible = false;
                Vaccine0Name.IsVisible = false;
                togSwitch4.IsVisible = false;
                Vaccine1Label.IsVisible = false;
                Vaccine1More.IsVisible = false;
                Vaccine1Name.IsVisible = false;
                togSwitch5.IsVisible = false;
                Vaccine2Label.IsVisible = false;
                Vaccine2More.IsVisible = false;
                Vaccine2Name.IsVisible = false;
                t4 = false;
                t5 = false;
            }
        }

        private void OnToggle4(object sender, EventArgs e)
        {
            t4 = !t4;
            if (t4)
            {
                Vaccine1Label.IsVisible = true;
                Vaccine2More.IsVisible = true;
                Vaccine1Name.IsVisible = true;
                togSwitch5.IsVisible = true;
            }
            else
            {
                Vaccine1Label.IsVisible = false;
                Vaccine2More.IsVisible = false;
                Vaccine1Name.IsVisible = false;
                togSwitch5.IsVisible = false;
                Vaccine2Label.IsVisible = false;
                Vaccine2Name.IsVisible = false;
                t5 = false;
            }
        }

        private void OnToggle5(object sender, EventArgs e)
        {
            t5 = !t5;

            if (t5)
            {
                Vaccine2Label.IsVisible = true;
                Vaccine2Name.IsVisible = true;
            }
            else
            {
                Vaccine2Label.IsVisible = false;
                Vaccine2Name.IsVisible = false;
            }
        }
        private void OnToggle6(object sender, EventArgs e)
        {
            t6 = !t6;

            if (t6)
            {
                FollowUpDateEntry.IsVisible = true;
                FollowUpDateLabel.IsVisible = true;
                FollowUpTimeEntry.IsVisible = true;
                FollowUpTimeLabel.IsVisible = true;
            }
            else
            {
                FollowUpDateEntry.IsVisible = false;
                FollowUpTimeEntry.IsVisible = false;
                FollowUpDateLabel.IsVisible = false;
                FollowUpTimeLabel.IsVisible = false;
            }
        }
    }
}