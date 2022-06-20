﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Repository.Migrations.MsgInChat
{
    [DbContext(typeof(MsgInChatContext))]
    partial class MsgInChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Models.MsgInChat", b =>
                {
                    b.Property<int?>("ChatId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ChatId");

                    b.ToTable("MsgInChat", (string)null);
                });

            modelBuilder.Entity("Models.MsgInChat", b =>
                {
                    b.HasOne("Models.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId");

                    b.Navigation("Chat");
                });
#pragma warning restore 612, 618
        }
    }
}
