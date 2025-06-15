import 'package:flavortalk_app/widgets/typography/base_typography.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class FlavorSubheading extends BaseTypography {
  const FlavorSubheading({required super.text, super.key});

  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: GoogleFonts.poppins(
        decoration: TextDecoration.none,
        color: Colors.black,
        fontSize: 18,
        fontWeight: FontWeight.w400,
      ),
    );
  }
}
